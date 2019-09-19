using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;

namespace CocShop.Data.Repositories
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
