using CocShop.Core.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Core.Service
{
    public interface IHubUserConnectionService
    {
        IQueryable<HubUserConnection> GetHubUserConnections();
        IQueryable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where);
        HubUserConnection GetHubUserConnection(Guid Id);
        void CreateHubUserConnection(HubUserConnection HubUserConnection);
        void UpdateHubUserConnection(HubUserConnection HubUserConnection);
        void DeleteHubUserConnection(HubUserConnection HubUserConnection);
        void DeleteHubUserConnection(Expression<Func<HubUserConnection, bool>> where);
        void Save();
    }
}
