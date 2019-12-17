using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CocShop.Core.Data.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using CocShop.Core.Data.Query;
using System.ComponentModel;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using CocShop.Core.Extentions;
using CocShop.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CocShop.Service.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        private readonly IStoreCategoryRepository _StoreCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IStoreCategoryRepository StoreCategoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _StoreCategoryRepository = StoreCategoryRepository;
        }



        public BaseViewModel<StoreViewModel> GetStore(Guid id, string include = null)
        {
            var includeList = IncludeLinqHelper<Store>.StringToListInclude(include);

            var Store = _repository.Get(_ => _.Id == id && _.IsDelete == false, includeList).FirstOrDefault();

            if (Store == null)
            {
                return new BaseViewModel<StoreViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<StoreViewModel>
            {
                Data = _mapper.Map<StoreViewModel>(Store),
            };
        }


        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStores(BasePagingRequestViewModel request)
        {
            return await GetAll(request, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);
        }

        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStoresNoPaging(BaseRequestViewModel request)
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


        private async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAll(BasePagingRequestViewModel request, string defaultCondition = null)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<StoreViewModel>>();

            string filter = SearchHelper<Store>.GenerateStringExpression(request.Filter, defaultCondition);

            Expression<Func<Store, bool>> FilterExpression = await LinqHelper<Store>.StringToExpression(filter);

            var includeList = IncludeLinqHelper<Store>.StringToListInclude(request?.Include);

            QueryArgs<Store> queryArgs = new QueryArgs<Store>
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
                result.Data = new PagingResult<StoreViewModel>
                {
                    Results = _mapper.Map<IEnumerable<StoreViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }

        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllNearestStore(GetNearestStoreRequestViewmovel request, string defaultCondition = null)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<StoreViewModel>>();





            var data = await _repository.GetAllNearestStore(request.Latitude, request.Longitude, request.Radius, pageSize * (pageIndex - 1), pageSize);

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
                result.Data = new PagingResult<StoreViewModel>
                {
                    Results = _mapper.Map<IEnumerable<StoreViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                };
            }

            return result;
        }


        public void Save()
        {
            _unitOfWork.Commit();
        }


        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetStoreByCategoryID(Guid cateId, BasePagingRequestViewModel request)
        {
            var cate = _StoreCategoryRepository.GetById(cateId);
            var listStore = _repository.GetMany(_ => _.IsDelete == false && _.Cate_Id == cateId);

            return await GetAll(request, $"{Constants.DEAFAULT_DELETE_STATUS_EXPRESSION} && _.CateId == new System.Guid(\"{cateId}\")");
        }

        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetTopStore(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<StoreViewModel>>();



            var data = await _repository.GetTopStore(pageSize * (pageIndex - 1), pageSize);

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
                result.Data = new PagingResult<StoreViewModel>
                {
                    Results = _mapper.Map<IEnumerable<StoreViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                };
            }

            return result;

        }
    }
}
