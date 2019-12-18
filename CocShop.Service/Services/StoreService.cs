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
using System.Device.Location;

namespace CocShop.Service.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IStoreCategoryRepository _StoreCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IStoreCategoryRepository StoreCategoryRepository, IProductRepository productRepository, IPromotionRepository promotionRepository)
        {
            _productRepository = productRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _StoreCategoryRepository = StoreCategoryRepository;
            _promotionRepository = promotionRepository;
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


        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStores(GetStoreWithGPSRequestViewmovel request)
        {
            return await GetAll(request, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);
        }

        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStoresNoPaging(GetStoreWithGPSRequestViewmovel request)
        {
            return await GetAll(new GetStoreWithGPSRequestViewmovel
            {
                PageIndex = null,
                PageSize = null,
                Filter = request.Filter,
                Include = request.Include,
                SortBy = request.SortBy,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            }, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);
        }


        private async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAll(GetStoreWithGPSRequestViewmovel request, string defaultCondition = null)
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
                foreach (var item in result.Data.Results)
                {
                    var listPromo = _promotionRepository.GetMany(_ => _.BrandId == item.BrandId && _.IsActive == true);
                    item.Promotion = _mapper.Map<ICollection<PromotionViewModel>>(listPromo);

                }
            }
            if (request.Longitude != null && request.Latitude != null)
            {
                foreach (var item in result?.Data?.Results)
                {
                    var sCoord = new GeoCoordinate(item.Latitude, item.Longitude);
                    var eCoord = new GeoCoordinate(request.Latitude.Value, request.Longitude.Value);

                    item.Distance = (sCoord.GetDistanceTo(eCoord) / 1000.0);

                }
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
                foreach (var item in result.Data.Results)
                {
                    var listPromo = _promotionRepository.GetMany(_ => _.BrandId == item.BrandId && _.IsActive == true);
                    item.Promotion = _mapper.Map<ICollection<PromotionViewModel>>(listPromo);

                }
            }

            return result;
        }


        public void Save()
        {
            _unitOfWork.Commit();
        }


        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetStoreByCategoryID(Guid cateId, GetStoreWithGPSRequestViewmovel request)
        {
            var cate = _StoreCategoryRepository.GetById(cateId);
            var listStore = _repository.GetMany(_ => _.IsDelete == false && _.Cate_Id == cateId);

            return await GetAll(request, $"{Constants.DEAFAULT_DELETE_STATUS_EXPRESSION} && _.CateId == new System.Guid(\"{cateId}\")");
        }

        public async Task<BaseViewModel<PagingResult<StoreViewModel>>> GetTopStore(GetStoreWithGPSRequestViewmovel request)
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

                foreach (var item in result.Data.Results)
                {
                    var listPromo = _promotionRepository.GetMany(_ => _.BrandId == item.BrandId && _.IsActive == true);
                    item.Promotion = _mapper.Map<ICollection<PromotionViewModel>>(listPromo);

                }
            }
            foreach (var item in result?.Data?.Results)
            {
                var sCoord = new GeoCoordinate(item.Latitude, item.Longitude);
                var eCoord = new GeoCoordinate(request.Latitude.Value, request.Longitude.Value);

                item.Distance = (sCoord.GetDistanceTo(eCoord) / 1000.0);

            }

            return result;

        }

        public async Task<BaseViewModel<StoreViewModel>> GetStoreInfor(Guid id, double? latitude, double? longitude)
        {

            var Store = _repository.GetMany(_ => _.Id == id && _.IsDelete == false)
                                        .Include(_ => _.StoreCategory)
                                        .Include(_ => _.MenuDishes).FirstOrDefault();

            foreach (var item in Store.MenuDishes)
            {
                item.Products = await _productRepository.GetMany(_ => _.IsDelete == false && _.MenuId == item.Id).ToListAsync();
            }
            if (Store == null)
            {
                return new BaseViewModel<StoreViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            var result = new BaseViewModel<StoreViewModel>
            {
                Data = _mapper.Map<StoreViewModel>(Store),
            };
            var listPromo = _promotionRepository.GetMany(_ => _.BrandId == result.Data.BrandId && _.IsActive == true);
            result.Data.Promotion = _mapper.Map<ICollection<PromotionViewModel>>(listPromo);
            if (latitude != null && longitude != null)
            {
                var sCoord = new GeoCoordinate(result.Data.Latitude, result.Data.Longitude);
                var eCoord = new GeoCoordinate(latitude.Value, longitude.Value);
                result.Data.Distance = (sCoord.GetDistanceTo(eCoord) / 1000.0);
            }
            return result;

        }
    }
}
