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

        public BaseViewModel<IEnumerable<OrderViewModel>> GetAllOrders()
        {
            var data = _repository.GetAll().Where(_ => _.IsDelete == false);

            if (data == null || !data.Any())
            {
                return new BaseViewModel<IEnumerable<OrderViewModel>>()
                {
                    Description = MessageHandler.CustomMessage(MessageConstants.NORECORD),
                    Code = MessageConstants.NORECORD
                };
            }

            return new BaseViewModel<IEnumerable<OrderViewModel>>()
            {
                Data = _mapper.Map<IEnumerable<OrderViewModel>>(data)
            };

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
