using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Repository;
using CocShop.Core.Service;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Service.Service
{
    public class HubUserConnectionService : IHubUserConnectionService
    {
        private readonly IHubUserConnectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public HubUserConnectionService(IHubUserConnectionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateHubUserConnection(HubUserConnection HubUserConnection)
        {
            _repository.Add(HubUserConnection);
        }

        public void DeleteHubUserConnection(HubUserConnection HubUserConnection)
        {
            _repository.Delete(HubUserConnection);
        }

        public void DeleteHubUserConnection(Expression<Func<HubUserConnection, bool>> where)
        {
            var HubUserConnections = _repository.GetMany(where);
            HubUserConnections.ToList().ForEach(e =>
            {
                _repository.Delete(e);
            });
        }

        public HubUserConnection GetHubUserConnection(Guid id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<HubUserConnection> GetHubUserConnections()
        {
            return _repository.GetAll();
        }

        public IQueryable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateHubUserConnection(HubUserConnection HubUserConnection)
        {
            _repository.Update(HubUserConnection);
        }
    }
}
