using CocShop.Core.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Core.Service
{
    public interface INotificationService
    {
        IQueryable<Notification> GetNotifications();
        IQueryable<Notification> GetNotifications(Expression<Func<Notification, bool>> where);
        Notification GetNotification(Guid Id);
        void CreateNotification(Notification Notification);
        void UpdateNotification(Notification Notification);
        void DeleteNotification(Notification Notification);
        void DeleteNotification(Expression<Func<Notification, bool>> where);
        void Save();
    }
}
