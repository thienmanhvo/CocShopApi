using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IOrderService
    {
        Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrdersByUser(BasePagingRequestViewModel request);
        Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrdersByAdmin(BasePagingRequestViewModel request);
        BaseViewModel<OrderViewModel> GetOrderByAdmin(Guid id, string include = null);
        BaseViewModel<string> CancelOrder(Guid id);
        BaseViewModel<string> PickOrder(Guid id);
        BaseViewModel<string> CompleteOrder(Guid id);
        BaseViewModel<OrderViewModel> GetOrderByUser(Guid id, string include = null);
        //BaseViewModel<IEnumerable<OrderViewModel>> GetOrderByID(Guid cateId);
        BaseViewModel<OrderViewModel> CreateOrder(CreateOrderRequestViewModel product);
        //BaseViewModel<OrderViewModel> UpdateOrder(Guid id, UpdateOrderRequestViewModel product);
        //BaseViewModel<string> DeleteOrder(Guid id);
        void Save();
    }
}
