using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Service
{
    public interface IOrderDetailService
    {
        BaseViewModel<OrderWithOrderDetailViewModel> GetAllDetailByAdmin(Guid id, string include = null);
        BaseViewModel<OrderWithOrderDetailViewModel> GetAllDetailByUser(Guid id,string include = null);
        //BaseViewModel<IEnumerable<OrderViewModel>> GetOrderByID(Guid cateId);
        BaseViewModel<IEnumerable<OrderDetailViewModel>> CreateDetail(IEnumerable<CreateOrderDetailViewModel> order);
        BaseViewModel<IEnumerable<OrderDetailViewModel>> UpdateDetail(Guid id, IEnumerable<CreateOrderDetailViewModel> order);
        //BaseViewModel<string> Delete(Guid id);
        void Save();
    }
}
