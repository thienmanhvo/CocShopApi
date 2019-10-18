using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface ILocationService
    {
        Task<BaseViewModel<PagingResult<LocationViewModel>>> GetAllLocations(BasePagingRequestViewModel request);
        Task<BaseViewModel<PagingResult<LocationViewModel>>> GetAllLoctionsNoPaging(BaseRequestViewModel request);
        BaseViewModel<LocationViewModel> GetLocation(Guid id);
        BaseViewModel<LocationViewModel> CreateLocation(CreateLocationRequestViewModel location);
        BaseViewModel<LocationViewModel> UpdateLocation(UpdateLocationRequestViewModel location);
        BaseViewModel<string> DeleteLocation(Guid id);
        void Save();
    }
}
