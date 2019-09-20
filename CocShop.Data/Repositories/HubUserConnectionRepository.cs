using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using System;

namespace CocShop.Data.Repositories
{
    public interface IHubUserConnectionRepository : IRepository<HubUserConnection>
    {

    }

    public class HubUserConnectionRepository : RepositoryBase<HubUserConnection>, IHubUserConnectionRepository
    {
        public HubUserConnectionRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
