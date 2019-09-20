using CocShop.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CocShop.Service.Service
{
    public interface IProductService
    {
        IQueryable<Product> GetProducts();
        IQueryable<Product> GetProducts(Expression<Func<Product, bool>> where);
        Product GetProduct(Guid id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteProduct(Expression<Func<Product, bool>> where);
        void Save();
    }
}
