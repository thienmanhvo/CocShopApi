using CocShop.Core.Data.Entity;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IProductCategoryService
    {
        Task<BaseViewModel<PagingResult<ProductCategoryViewModel>>> GetAllProductCategories(BasePagingRequestViewModel request);
        BaseViewModel<ProductCategoryViewModel> GetProductCategory(Guid id);
        BaseViewModel<ProductCategoryViewModel> CreateProductCategory(CreateProductCategoryRequestViewModel request);
        BaseViewModel<ProductCategoryViewModel> UpdateProductCategory(UpdateProductCategoryViewModel productCategory);
        BaseViewModel<string> DeleteProductCategory(Guid id);
        void Save();
    }
}
