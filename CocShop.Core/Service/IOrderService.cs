using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Service
{
    public interface IOrderService
    {
        BaseViewModel<IEnumerable<OrderViewModel>> GetAllOrders();
        BaseViewModel<OrderViewModel> GetOrder(Guid id);
        //BaseViewModel<IEnumerable<OrderViewModel>> GetOrderByID(Guid cateId);
        BaseViewModel<OrderViewModel> CreateOrder(CreateOrderRequestViewModel product);
        BaseViewModel<OrderViewModel> UpdateOrder(Guid id, UpdateOrderRequestViewModel product);
        BaseViewModel<string> DeleteOrder(Guid id);
        void Save();
    }
}
