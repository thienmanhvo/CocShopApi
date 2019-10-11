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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace CocShop.Service.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IProductCategoryRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public BaseViewModel<ProductCategoryViewModel> CreateProductCategory(CreateProductCategoryRequestViewModel request)
        {
            var entity = _mapper.Map<ProductCategory>(request);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<ProductCategoryViewModel>()
            {
                Data = _mapper.Map<ProductCategoryViewModel>(entity),
                StatusCode = HttpStatusCode.OK
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteProductCategory(Guid id)
        {
            //Find product
            var product = _repository.GetById(id);
            //result to return
            BaseViewModel<string> result = null;
            //check product exist
            if (product == null || product.IsDelete)
            {
                result = new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = ErrMessageConstants.NOTFOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND)
                };
            }
            else
            {
                //update column isDelete = true
                product.IsDelete = true;
                _repository.Update(product);
                result = new BaseViewModel<string>();
                //save change
                Save();
            }

            return result;
        }

        public BaseViewModel<ProductCategoryViewModel> GetProductCategory(Guid id)
        {
            var productCategory = _repository.GetById(id);

            if (productCategory == null || productCategory.IsDelete)
            {
                return new BaseViewModel<ProductCategoryViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<ProductCategoryViewModel>
            {
                Data = _mapper.Map<ProductCategoryViewModel>(productCategory),
            };
        }

        public async Task<BaseViewModel<PagingResult<ProductCategoryViewModel>>> GetAllProductCategories(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<ProductCategoryViewModel>>();
            string filter = SearchHelper<ProductCategory>.GenerateStringExpression(request.Filter, Constants.DEAFAULT_DELETE_STATUS_EXPRESSION);

            Expression<Func<ProductCategory, bool>> FilterExpression = await LinqHelper<ProductCategory>.StringToExpression(filter);

            QueryArgs<ProductCategory> queryArgs = new QueryArgs<ProductCategory>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
            };

            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit).ToList();

            if (data == null || !data.Any())
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
                result.Data = new PagingResult<ProductCategoryViewModel>
                {
                    Results = _mapper.Map<IEnumerable<ProductCategoryViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<ProductCategoryViewModel> UpdateProductCategory(UpdateProductCategoryViewModel productCategory)
        {
            var entity = _repository.GetById(productCategory.Id);
            if (entity == null || entity.IsDelete)
            {
                return new BaseViewModel<ProductCategoryViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(productCategory, entity);

            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<ProductCategoryViewModel>
            {
                Data = _mapper.Map<ProductCategoryViewModel>(entity),
            };

            Save();

            return result;
        }
    }
}
