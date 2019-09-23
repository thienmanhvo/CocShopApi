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

        public BaseViewModel<ProductCategoryViewModel> CreateProductCategory(ProductCategoryCreateRequest request)
        {
            var entity = _mapper.Map<ProductCategory>(request);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<ProductCategoryViewModel>()
            {
                Data = _mapper.Map<ProductCategoryViewModel>(entity),
                Message = MessageHandler.CustomMessage(MessageConstants.SUCCESS),
                StatusCode = HttpStatusCode.OK
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteProductCategory(string id)
        {
            //Find product
            var product = _repository.GetById(new Guid(id));
            //result to return
            BaseViewModel<string> result;
            //check product exist
            if (product == null || product.IsDelete)
            {
                result = new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND)
                };
            }
            else
            {
                //update column isDelete = true
                product.IsDelete = true;
                _repository.Update(product);


                result = new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = MessageHandler.CustomMessage(MessageConstants.SUCCESS)
                };
                //save change
                Save();
            }

            return result;
        }

        public BaseViewModel<ProductCategoryViewModel> GetProductCategory(string id)
        {
            var productCategory = _repository.GetById(new Guid(id));

            if (productCategory == null || productCategory.IsDelete)
            {
                return new BaseViewModel<ProductCategoryViewModel>
                {
                    Message = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new BaseViewModel<ProductCategoryViewModel>
            {
                Data = _mapper.Map<ProductCategoryViewModel>(productCategory),
                Message = MessageHandler.CustomMessage(MessageConstants.SUCCESS),
                StatusCode = HttpStatusCode.OK
            };
        }

        public BaseViewModel<IEnumerable<ProductCategoryViewModel>> GetAllProductCategories()
        {
            var data = _repository.GetMany(_ => _.IsDelete == false);//.Include(_ => _.Product);

            if (data == null || !data.Any())
            {
                return new BaseViewModel<IEnumerable<ProductCategoryViewModel>>()
                {
                    Message = MessageHandler.CustomMessage(MessageConstants.NORECORD),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new BaseViewModel<IEnumerable<ProductCategoryViewModel>>()
            {
                Data = _mapper.Map<IEnumerable<ProductCategoryViewModel>>(data),
                Message = MessageHandler.CustomMessage(MessageConstants.SUCCESS),
                StatusCode = HttpStatusCode.OK
            };
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateProductCategory(ProductCategory ProductCategory)
        {
            _repository.Update(ProductCategory);
        }
    }
}
