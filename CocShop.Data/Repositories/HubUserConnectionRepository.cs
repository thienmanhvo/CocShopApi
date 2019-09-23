using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Repository;
using System;

namespace CocShop.Repository.Repositories
{
    public class HubUserConnectionRepository : RepositoryBase<HubUserConnection>, IHubUserConnectionRepository
    {
        public HubUserConnectionRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
