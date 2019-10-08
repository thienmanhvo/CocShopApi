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
        BaseViewModel<ProductViewModel> GetProduct(Guid id);
        BaseViewModel<IEnumerable<ProductViewModel>> GetProductByCategoryID(Guid cateId);
        BaseViewModel<ProductViewModel> CreateProduct(CreateProductRequestViewModel product);
        BaseViewModel<ProductViewModel> UpdateProduct(Guid id, UpdateProductRequestViewModel product);
        BaseViewModel<string> DeleteProduct(Guid id);
        void Save();
    }
}
