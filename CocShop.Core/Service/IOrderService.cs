using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Service
{
    public interface IOrderService
    {
        BaseViewModel<IEnumerable<ProductViewModel>> GetAllOrders();
        BaseViewModel<ProductViewModel> GetOrder(Guid id);
        BaseViewModel<IEnumerable<ProductViewModel>> GetOrderByID(Guid cateId);
        BaseViewModel<ProductViewModel> CreateOrder(CreateProductRequestViewModel product);
        BaseViewModel<ProductViewModel> UpdateOrder(Guid id, UpdateProductRequestViewModel product);
        BaseViewModel<string> DeleteOrder(Guid id);
        void Save();
    }
}
