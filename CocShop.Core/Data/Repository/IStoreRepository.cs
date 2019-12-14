using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Data.Repository
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<ICollection<Store>> GetAllNearestStore(double latpoint, double longpoint, double radius, int? offset = null, int? limit = null);
    }
}
