using AutoMapper;
using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using CocShop.Data.Repositories;
using CocShop.Service.ViewModel;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Service.Service
{
    public interface IProductService
    {
        IQueryable<Product> GetProducts();
        IQueryable<Product> GetProducts(Expression<Func<Product, bool>> where);
        Product GetProduct(Guid id);
        Product CreateProduct(ProductRequestViewModel product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteProduct(Expression<Func<Product, bool>> where);
        void Save();
    }
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

        public Product CreateProduct(ProductRequestViewModel Product)
        {
            var entity = _mapper.Map<Product>(Product);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            return entity;
        }

        public void DeleteProduct(Product Product)
        {
            _repository.Delete(Product);
        }

        public void DeleteProduct(Expression<Func<Product, bool>> where)
        {
            _repository.Delete(where);
        }

        public Product GetProduct(Guid id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _repository.GetAll().Where(_ => _.IsDelete == false);
        }

        public IQueryable<Product> GetProducts(Expression<Func<Product, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateProduct(Product Product)
        {
            _repository.Update(Product);
        }
    }
}
