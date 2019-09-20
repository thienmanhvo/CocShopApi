using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
