using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IMyUserService
    {
        Task<BaseViewModel<PagingResult<MyUserViewModel>>> GetAllMyUsers(BasePagingRequestViewModel request);
        BaseViewModel<MyUserViewModel> GetMyUser(Guid id);
        void Save();
    }
}
