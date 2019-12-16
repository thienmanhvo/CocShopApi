using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Query;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IStoreRepository _storeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IStoreRepository storeRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }



        public BaseViewModel<BrandViewModel> GetBrand(Guid id, string include = null)
        {
            var includeList = IncludeLinqHelper<Brand>.StringToListInclude(include);

            var Brand = _repository.Get(_ => _.Id == id && _.IsDelete == false, includeList).FirstOrDefault();

            if (Brand == null)
            {
                return new BaseViewModel<BrandViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<BrandViewModel>
            {
                Data = _mapper.Map<BrandViewModel>(Brand),
            };
        }


        public async Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllBrands(BasePagingRequestViewModel request)
        {
            return await GetAll(request, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);
        }

        public async Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllBrandsNoPaging(BaseRequestViewModel request)
        {
            return await GetAll(new BasePagingRequestViewModel
            {
                PageIndex = null,
                PageSize = null,
                Filter = request.Filter,
                Include = request.Include,
                SortBy = request.SortBy
            }, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);
        }


        private async Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAll(BasePagingRequestViewModel request, string defaultCondition = null)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<BrandViewModel>>();

            string filter = SearchHelper<Brand>.GenerateStringExpression(request.Filter, defaultCondition);

            Expression<Func<Brand, bool>> FilterExpression = await LinqHelper<Brand>.StringToExpression(filter);

            var includeList = IncludeLinqHelper<Brand>.StringToListInclude(request?.Include);

            QueryArgs<Brand> queryArgs = new QueryArgs<Brand>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
                Include = includeList
            };


            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit, queryArgs.Include).ToList();

            //var sql = data.ToSql();

            if (data == null || data.Count == 0)
            {
                result.Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD);
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {
                var pageSizeReturn = pageSize;
                if (data.Count < pageSize)
                {
                    pageSizeReturn = data.Count;
                }
                result.Data = new PagingResult<BrandViewModel>
                {
                    Results = _mapper.Map<IEnumerable<BrandViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            foreach (var item in result.Data.Results)
            {
                var listStore = _storeRepository.GetMany(_ => _.BrandId == item.Id).ToList();
                var sumRating = listStore.Sum(_ => _.Rating);
                var sumNumberOfRating = listStore.Sum(_ => _.NumberOfRating);
                item.Location = listStore.Count;
                item.Rating = sumRating * 1.0 / sumNumberOfRating * 1.0;
            }

            return result;
        }

        //public async Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllNearestBrand(GetNearestBrandRequestViewmovel request, string defaultCondition = null)
        //{
        //    var pageSize = request.PageSize;
        //    var pageIndex = request.PageIndex;
        //    var result = new BaseViewModel<PagingResult<BrandViewModel>>();





        //    var data = await _repository.GetAllNearestBrand(request.Latitude, request.Longitude, request.Radius, pageSize * (pageIndex - 1), pageSize);

        //    //var sql = data.ToSql();

        //    if (data == null || data.Count == 0)
        //    {
        //        result.Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD);
        //        result.Code = MessageConstants.NO_RECORD;
        //    }
        //    else
        //    {
        //        var pageSizeReturn = pageSize;
        //        if (data.Count < pageSize)
        //        {
        //            pageSizeReturn = data.Count;
        //        }
        //        result.Data = new PagingResult<BrandViewModel>
        //        {
        //            Results = _mapper.Map<IEnumerable<BrandViewModel>>(data),
        //            PageIndex = pageIndex,
        //            PageSize = pageSizeReturn,
        //        };
        //    }

        //    return result;
        //}


        public void Save()
        {
            _unitOfWork.Commit();
        }


        //public async Task<BaseViewModel<PagingResult<BrandViewModel>>> GetBrandByCategoryID(Guid cateId, BasePagingRequestViewModel request)
        //{
        //    var cate = _BrandCategoryRepository.GetById(cateId);
        //    var listBrand = _repository.GetMany(_ => _.IsDelete == false && _.Cate_Id == cateId);

        //    return await GetAll(request, $"{Constants.DEAFAULT_DELETE_STATUS_EXPRESSION} && _.CateId == new System.Guid(\"{cateId}\")");
        //}
    }
}

