using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IBrandService
    {
        Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllBrands(BasePagingRequestViewModel request);
        //Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllBrandsNoPaging(BaseRequestViewModel request);
        //Task<BaseViewModel<PagingResult<BrandViewModel>>> GetAllNearestBrands(GetNearestStoreRequestViewmovel request, string defaultCondition = null);
        //BaseViewModel<StoreViewModel> GetStore(Guid id, string include = null);
        //Task<BaseViewModel<PagingResult<StoreViewModel>>> GetStoreByCategoryID(Guid cateId, BasePagingRequestViewModel request);
        //BaseViewModel<StoreViewModel> CreateStore(CreateStoreRequestViewModel Store);
        //BaseViewModel<StoreViewModel> UpdateStore(Guid id, UpdateStoreRequestViewModel Store);
        //BaseViewModel<string> DeleteStore(Guid id);
        void Save();
    }
}
