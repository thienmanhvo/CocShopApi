using CocShop.Core.Data.Entity;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;

namespace CocShop.Core.Service
{
    public interface IProductCategoryService
    {
        BaseViewModel<IEnumerable<ProductCategoryViewModel>> GetAllProductCategories();
        BaseViewModel<ProductCategoryViewModel> GetProductCategory(Guid id);
        BaseViewModel<ProductCategoryViewModel> CreateProductCategory(ProductCategoryCreateRequest request);
        void UpdateProductCategory(ProductCategory ProductCategory);
        BaseViewModel<string> DeleteProductCategory(Guid id);
        void Save();
    }
}
