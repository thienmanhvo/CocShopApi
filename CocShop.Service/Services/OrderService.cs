using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CocShop.Core.Data.Entity;
using CocShop.Service.Helpers;
using System.Threading.Tasks;
using CocShop.Core.Data.Query;

namespace CocShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseViewModel<OrderViewModel> CreateOrder(CreateOrderRequestViewModel Order)
        {
            var entity = _mapper.Map<Order>(Order);
            entity.Id = Guid.NewGuid();
            entity.SetDefaultInsertValue(_repository.GetUsername());
            _repository.Add(entity);

            var result = new BaseViewModel<OrderViewModel>()
            {
                Data = _mapper.Map<OrderViewModel>(entity),
            };

            Save();

            return result;
        }

        public BaseViewModel<string> DeleteOrder(Guid id)
        {
            //Find product
            var order = _repository.GetById(id);
            //result to return
            BaseViewModel<string> result = null;
            //check product exist
            if (order == null || order.IsDelete)
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
                order.IsDelete = true;
                _repository.Update(order);
                result = new BaseViewModel<string>();
                //save change
                Save();
            }
            return result;
        }

        public BaseViewModel<OrderViewModel> GetOrder(Guid id)
        {
            var order = _repository.GetById(id);

            if (order == null || order.IsDelete)
            {
                return new BaseViewModel<OrderViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            return new BaseViewModel<OrderViewModel>
            {
                Data = _mapper.Map<OrderViewModel>(order),
            };
        }

        public async Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrders(BasePagingRequestViewModel request)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<OrderViewModel>>();
            string filter = SearchHelper<Order>.GenerateStringExpression(request.Filter);

            Expression<Func<Order, bool>> FilterExpression = await LinqHelper<Order>.StringToExpression(filter);


            QueryArgs<Order> queryArgs = new QueryArgs<Order>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
            };

            var data = _repository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit).ToList();

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
                result.Data = new PagingResult<OrderViewModel>
                {
                    Results = _mapper.Map<IEnumerable<OrderViewModel>>(data),
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

        public BaseViewModel<OrderViewModel> UpdateOrder(Guid id, UpdateOrderRequestViewModel order)
        {
            var entity = _repository.GetById(id);
            if (entity == null || entity.IsDelete)
            {
                return new BaseViewModel<OrderViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }

            entity = _mapper.Map(order, entity);

            entity.SetDefaultUpdateValue(_repository.GetUsername());
            _repository.Update(entity);
            var result = new BaseViewModel<OrderViewModel>
            {
                Data = _mapper.Map<OrderViewModel>(entity),
            };

            Save();

            return result;
        }

    }
}
