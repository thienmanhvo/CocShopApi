using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        public BaseViewModel<IEnumerable<ProductCategoryViewModel>> GetAllProductCategories()
        {
            var data = _repository.GetMany(_ => _.IsDelete == false);//.Include(_ => _.Product);

            if (data == null || !data.Any())
            {
                return new BaseViewModel<IEnumerable<ProductCategoryViewModel>>()
                {
                    Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD),
                    Code = MessageConstants.NO_RECORD
                };
            }

            return new BaseViewModel<IEnumerable<ProductCategoryViewModel>>()
            {
                Data = _mapper.Map<IEnumerable<ProductCategoryViewModel>>(data),
            };
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
