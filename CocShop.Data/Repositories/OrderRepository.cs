using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using System;

namespace CocShop.Repository.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
