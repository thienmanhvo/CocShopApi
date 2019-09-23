using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.Service;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Service.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateNotification(Notification Notification)
        {
            _repository.Add(Notification);
        }

        public void DeleteNotification(Notification Notification)
        {
            _repository.Delete(Notification);
        }

        public void DeleteNotification(Expression<Func<Notification, bool>> where)
        {
            _repository.Delete(where);
        }

        public Notification GetNotification(Guid id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Notification> GetNotifications()
        {
            return _repository.GetAll();
        }

        public IQueryable<Notification> GetNotifications(Expression<Func<Notification, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateNotification(Notification Notification)
        {
            _repository.Update(Notification);
        }
    }
}
