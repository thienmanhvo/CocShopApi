﻿using AutoMapper;
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

namespace CocShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel Product)
        {
            var entity = _mapper.Map<Product>(Product);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<ProductViewModel>()
            {
                Data = _mapper.Map<ProductViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteProduct(Guid id)
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

        public BaseViewModel<ProductViewModel> GetProduct(Guid id)
        {
            var product = _repository.GetById(id);

            if (product == null || product.IsDelete)
            {
                return new BaseViewModel<ProductViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<ProductViewModel>
            {
                Data = _mapper.Map<ProductViewModel>(product),
            };
        }

        public BaseViewModel<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var data = _repository.GetAll().Where(_ => _.IsDelete == false);

            if (data == null || !data.Any())
            {
                return new BaseViewModel<IEnumerable<ProductViewModel>>()
                {
                    Description = MessageHandler.CustomMessage(MessageConstants.NORECORD),
                    Code = MessageConstants.NORECORD
                };
            }

            return new BaseViewModel<IEnumerable<ProductViewModel>>()
            {
                Data = _mapper.Map<IEnumerable<ProductViewModel>>(data)
            };

        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<ProductViewModel> UpdateProduct(Guid id, UpdateProductRequestViewModel product)
        {
            var entity = _repository.GetById(id);
            if (entity == null || entity.IsDelete)
            {
                return new BaseViewModel<ProductViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(product, entity);

            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<ProductViewModel>
            {
                Data = _mapper.Map<ProductViewModel>(entity),
            };

            Save();

            return result;
        }
    }
}
