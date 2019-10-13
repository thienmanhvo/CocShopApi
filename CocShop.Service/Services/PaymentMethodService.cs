using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Query;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Service.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {

        private readonly IPaymentMethodRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentMethodRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseViewModel<PaymentMethodViewModel> CreatePaymentMethod(CreatePaymentMethodRequestViewModel paymentMethod)
        {
            var entity = _mapper.Map<PaymentMethod>(paymentMethod);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            entity.UserId = new Guid(_repository.GetCurrentUserId());
            _repository.Add(entity);

            var result = new BaseViewModel<PaymentMethodViewModel>()
            {
                Data = _mapper.Map<PaymentMethodViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeletePaymentMethod(Guid id)
        {
            //Find PaymentMethod
            var userId = new Guid(_repository.GetCurrentUserId());
            var paymentMethod = _repository.GetMany(_ => _.IsDelete == false && _.UserId == userId && _.Id == id).FirstOrDefault();
            //result to return
            BaseViewModel<string> result = null;
            //check PaymentMethod exist
            if (paymentMethod == null)
            {
                result = new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = ErrMessageConstants.NOTFOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND)
                };
            }
            else
            {
                //update column isDelete = true
                paymentMethod.IsDelete = true;
                _repository.Update(paymentMethod);
                result = new BaseViewModel<string>();
                //save change
                Save();
            }
            return result;
        }

        public BaseViewModel<PaymentMethodViewModel> GetPaymentMethod(Guid id)
        {
            var userId = new Guid(_repository.GetCurrentUserId());
            var paymentMethod = _repository.GetMany(_ => _.IsDelete == false && _.UserId == userId && _.Id == id).FirstOrDefault();

            if (paymentMethod == null)
            {
                return new BaseViewModel<PaymentMethodViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<PaymentMethodViewModel>
            {
                Data = _mapper.Map<PaymentMethodViewModel>(paymentMethod),
            };
        }

        public async Task<BaseViewModel<PagingResult<PaymentMethodViewModel>>> GetAllPaymentMethods(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<PaymentMethodViewModel>>();
            var currentUserId = _repository.GetCurrentUserId();
            var defaultFilter = $"_.UserId.Equals(new System.Guid(\"{currentUserId}\")) && {Constants.DEAFAULT_DELETE_STATUS_EXPRESSION}";
            string filter = SearchHelper<PaymentMethod>.GenerateStringExpression(request.Filter, defaultFilter);

            Expression<Func<PaymentMethod, bool>> FilterExpression = await LinqHelper<PaymentMethod>.StringToExpression(filter);

            QueryArgs<PaymentMethod> queryArgs = new QueryArgs<PaymentMethod>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
            };


            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit).ToList();

            //var sql = data.ToSql();

            if (data == null || data.Count == 0)
            {
                result.Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD);
                result.Code = MessageConstants.NO_RECORD;
            }
            else
            {
                var pageSizeReturn = pageSize;
                if (data.Count < pageSize)
                {
                    pageSizeReturn = data.Count;
                }
                result.Data = new PagingResult<PaymentMethodViewModel>
                {
                    Results = _mapper.Map<IEnumerable<PaymentMethodViewModel>>(data),
                    PageIndex = pageIndex,
                    PageSize = pageSizeReturn,
                    TotalRecords = _repository.Count(queryArgs.Filter)
                };
            }

            return result;
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<PaymentMethodViewModel> UpdatePaymentMethod(UpdatePaymentMethodRequestViewModel paymentMethod)
        {
            var userId = new Guid(_repository.GetCurrentUserId());
            var entity = _repository.GetMany(_ => _.IsDelete == false && _.UserId == userId && _.Id == paymentMethod.Id).FirstOrDefault();
            if (entity == null)
            {
                return new BaseViewModel<PaymentMethodViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(paymentMethod, entity);

            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<PaymentMethodViewModel>
            {
                Data = _mapper.Map<PaymentMethodViewModel>(entity),
            };

            Save();

            return result;
        }
    }
}
