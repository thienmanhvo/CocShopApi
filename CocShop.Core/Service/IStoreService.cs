using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IStoreService
    {
        Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStores(GetStoreWithGPSRequestViewmovel request);
        Task<BaseViewModel<PagingResult<StoreViewModel>>> GetTopStore(GetStoreWithGPSRequestViewmovel request);
        Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllStoresNoPaging(GetStoreWithGPSRequestViewmovel request);
        Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllNearestStore(GetNearestStoreRequestViewmovel request, string defaultCondition = null);
        BaseViewModel<StoreViewModel> GetStore(Guid id, string include = null);
        Task<BaseViewModel<StoreViewModel>> GetStoreInfor(Guid id);
        //Task<BaseViewModel<PagingResult<StoreViewModel>>> GetStoreByCategoryID(Guid cateId, BasePagingRequestViewModel request);
        //BaseViewModel<StoreViewModel> CreateStore(CreateStoreRequestViewModel Store);
        //BaseViewModel<StoreViewModel> UpdateStore(Guid id, UpdateStoreRequestViewModel Store);
        //BaseViewModel<string> DeleteStore(Guid id);
        void Save();
    }
}
