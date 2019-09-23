using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Repository;
using System;

namespace CocShop.Data.Repositories
{
    public class HubUserConnectionRepository : RepositoryBase<HubUserConnection>, IHubUserConnectionRepository
    {
        public HubUserConnectionRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
