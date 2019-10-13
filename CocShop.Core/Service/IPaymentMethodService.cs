using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Core.Service
{
    public interface IPaymentMethodService
    {
        Task<BaseViewModel<PagingResult<PaymentMethodViewModel>>> GetAllPaymentMethods(BasePagingRequestViewModel request);
        BaseViewModel<PaymentMethodViewModel> GetPaymentMethod(Guid id);
        BaseViewModel<PaymentMethodViewModel> CreatePaymentMethod(CreatePaymentMethodRequestViewModel paymentMethod);
        BaseViewModel<PaymentMethodViewModel> UpdatePaymentMethod(UpdatePaymentMethodRequestViewModel paymentMethod);
        BaseViewModel<string> DeletePaymentMethod(Guid id);
        void Save();
    }
}
