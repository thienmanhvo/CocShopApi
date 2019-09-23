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
        BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel product);
        BaseViewModel<ProductViewModel> UpdateProduct(string id, UpdateProductRequestViewModel product);
        BaseViewModel<string> DeleteProduct(string id);
        void Save();
    }
}
