using CocShop.Data.Entity;
using CocShop.Data.Infrastructure;
using System;

namespace CocShop.Data.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {

    }

    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
