using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {

    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
