using CocShop.Core.Infrastructure;
using CocShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Repositories
{
    public interface IHubUserConnectionRepository : IRepository<HubUserConnection>
    {

    }

    public class HubUserConnectionRepository : RepositoryBase<HubUserConnection>, IHubUserConnectionRepository
    {
        public HubUserConnectionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
