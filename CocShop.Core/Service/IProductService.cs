using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Service
{
    public interface IProductService
    {
        BaseViewModel<IEnumerable<ProductViewModel>> GetAllProducts();
        BaseViewModel<ProductViewModel> GetProduct(Guid id);
        BaseViewModel<IEnumerable<ProductViewModel>> GetProductByCategoryID(Guid cateId);
        BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel product);
        BaseViewModel<ProductViewModel> UpdateProduct(Guid id, UpdateProductRequestViewModel product);
        BaseViewModel<string> DeleteProduct(Guid id);
        void Save();
    }
}
