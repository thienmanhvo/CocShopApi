using CocShop.Core.Data.Entity;
using CocShop.Core.ViewModel;
using System.Collections.Generic;

namespace CocShop.Core.Service
{
    public interface IProductCategoryService
    {
        BaseViewModel<IEnumerable<ProductCategoryViewModel>> GetAllProductCategories();
        BaseViewModel<ProductCategoryViewModel> GetProductCategory(string id);
        BaseViewModel<ProductCategoryViewModel> CreateProductCategory(ProductCategoryCreateRequest request);
        void UpdateProductCategory(ProductCategory ProductCategory);
        BaseViewModel<string> DeleteProductCategory(string id);
        void Save();
    }
}
