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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public BaseViewModel<IEnumerable<OrderDetailViewModel>> CreateDetail(IEnumerable<CreateOrderDetailViewModel> details)
        {
            var entities = _mapper.Map<IEnumerable<OrderDetail>>(details);
            foreach(var entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.SetDefaultInsertValue(_repository.GetUsername());
                _repository.Add(entity);
            }

            var result = new BaseViewModel<IEnumerable<OrderDetailViewModel>>()
            {
                Data = _mapper.Map<IEnumerable<OrderDetailViewModel>>(entities),
            };

            Save();

            return result;
        }

        //public BaseViewModel<string> DeleteOrder(Guid id)
        //{
        //    //Find product
        //    var order = _repository.GetById(id);
        //    //result to return
        //    BaseViewModel<string> result = null;
        //    //check product exist
        //    if (order == null || order.IsDelete)
        //    {
        //        result = new BaseViewModel<string>()
        //        {
        //            StatusCode = HttpStatusCode.NotFound,
        //            Code = ErrMessageConstants.NOTFOUND,
        //            Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND)
        //        };
        //    }
        //    else
        //    {
        //        //update column isDelete = true
        //        order.IsDelete = true;
        //        _repository.Update(order);
        //        result = new BaseViewModel<string>();
        //        //save change
        //        Save();
        //    }
        //    return result;
        //}

        public BaseViewModel<IEnumerable<OrderDetailViewModel>> GetAllDetail(Guid id)
        {
            var detail = _repository.GetAll().Where(x => x.OrderId == id);
            
            if (detail == null)
            {
                return new BaseViewModel<IEnumerable<OrderDetailViewModel>>()
                {
                    Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD),
                    Code = MessageConstants.NO_RECORD
                };
            }

            return new BaseViewModel<IEnumerable<OrderDetailViewModel>>
            {
                Data = _mapper.Map<IEnumerable<OrderDetailViewModel>>(detail),
            };
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public BaseViewModel<IEnumerable<OrderDetailViewModel>> UpdateDetail(Guid id, IEnumerable<CreateOrderDetailViewModel> details)
        {
            var result = new BaseViewModel<IEnumerable<OrderDetailViewModel>>();
            if (details != null)
            {
                var listDetail = _repository.GetAll().Where(x => x.OrderId == id);

                foreach (var detail in listDetail)
                {
                    _repository.Delete(detail);
                }
                var entities = _mapper.Map<IEnumerable<OrderDetail>>(details);
                foreach (var entity in entities)
                {
                    entity.Id = Guid.NewGuid();
                    entity.SetDefaultInsertValue(_repository.GetUsername());
                    _repository.Add(entity);
                }

                if (entities == null)
                {
                    return new BaseViewModel<IEnumerable<OrderDetailViewModel>>()
                    {
                        Description = MessageHandler.CustomMessage(MessageConstants.NO_RECORD),
                        Code = MessageConstants.NO_RECORD
                    };
                }

                result = new BaseViewModel<IEnumerable<OrderDetailViewModel>>
                {
                    Data = _mapper.Map<IEnumerable<OrderDetailViewModel>>(entities),
                };
            }

            Save();

            return result;
        }

    }
}
