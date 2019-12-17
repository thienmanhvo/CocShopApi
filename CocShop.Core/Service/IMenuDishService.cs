using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IMenuDishService
    {
        Task<BaseViewModel<PagingResult<StoreViewModel>>> GetAllMenu(BasePagingRequestViewModel request);
        void Save();
    }
}
