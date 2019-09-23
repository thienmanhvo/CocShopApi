using AutoMapper;
using CocShop.Core.ViewModel;
using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using CocShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Service.Service
{
    public interface IProductCategoryService
    {
        IQueryable<ProductCategory> GetProductCategories();
        IQueryable<ProductCategory> GetProductCategories(Expression<Func<ProductCategory, bool>> where);
        ProductCategory GetProductCategory(Guid id);
        ProductCategory CreateProductCategory(ProductCategoryCreateRequest request);
        void UpdateProductCategory(ProductCategory ProductCategory);
        void DeleteProductCategory(ProductCategory ProductCategory);
        void DeleteProductCategory(Expression<Func<ProductCategory, bool>> where);
        void Save();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductCategory CreateProductCategory(ProductCategoryCreateRequest request)
        {
            var entity = _mapper.Map<ProductCategory>(request);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            return entity;
        }

        public void DeleteProductCategory(ProductCategory ProductCategory)
        {
            _repository.Delete(ProductCategory);
        }

        public void DeleteProductCategory(Expression<Func<ProductCategory, bool>> where)
        {
            _repository.Delete(where);
        }

        public ProductCategory GetProductCategory(Guid id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<ProductCategory> GetProductCategories()
        {
            return _repository.GetAll()
                .Where(_ => _.IsDelete == false)
                .Include(_ => _.Product);
        }

        public IQueryable<ProductCategory> GetProductCategories(Expression<Func<ProductCategory, bool>> where)
        {
            return _repository.GetMany(where);
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
