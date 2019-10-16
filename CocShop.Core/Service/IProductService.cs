using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IProductService
    {
        Task<BaseViewModel<PagingResult<ProductViewModel>>> GetAllProducts(BasePagingRequestViewModel request);
        Task<BaseViewModel<PagingResult<ProductViewModel>>> GetAllProductsNoPaging(BaseRequestViewModel request);
        BaseViewModel<ProductViewModel> GetProduct(Guid id);
        Task<BaseViewModel<PagingResult<ProductViewModel>>> GetProductByCategoryID(Guid cateId, BasePagingRequestViewModel request);
        BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel product);
        BaseViewModel<ProductViewModel> UpdateProduct(Guid id, UpdateProductRequestViewModel product);
        BaseViewModel<string> DeleteProduct(Guid id);
        void Save();
    }
}
