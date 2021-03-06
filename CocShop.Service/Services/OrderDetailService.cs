﻿using AutoMapper;
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

namespace CocShop.Service.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetaiRrepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderDetaiRrepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public BaseViewModel<IEnumerable<OrderDetailViewModel>> CreateDetail(IEnumerable<CreateOrderDetailViewModel> details)
        {
            var entities = _mapper.Map<IEnumerable<OrderDetail>>(details);
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.SetDefaultInsertValue(_orderDetaiRrepository.GetUsername());
                _orderDetaiRrepository.Add(entity);
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
        //    var order = _orderDetaiRrepository.GetById(id);
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
        //        _orderDetaiRrepository.Update(order);
        //        result = new BaseViewModel<string>();
        //        //save change
        //        Save();
        //    }
        //    return result;
        //}

        public BaseViewModel<OrderWithOrderDetailViewModel> GetOrderDetail(Guid id,Expression<Func<OrderDetail, bool>> filter, string include = null)
        {
            var includeList = IncludeLinqHelper<OrderDetail>.StringToListInclude(include);

            var orderDetail = _orderDetaiRrepository.Get(filter, includeList).ToList();

            if (orderDetail == null || orderDetail.Count == 0)
            {
                return new BaseViewModel<OrderWithOrderDetailViewModel>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                    Code = ErrMessageConstants.NOTFOUND
                };
            }
            var order = _orderRepository.Get(_ => _.Id == id);
            return new BaseViewModel<OrderWithOrderDetailViewModel>
            {
                Data = new OrderWithOrderDetailViewModel()
                {
                    TotalPrice = order.TotalPrice ?? 0,
                    ToTalQuantity = order.TotalQuantity ?? 0,
                    OrderDetail = _mapper.Map<IEnumerable<OrderDetailViewModel>>(orderDetail),
                    OrderStatus = order.Status
                }
            };
        }

        public BaseViewModel<OrderWithOrderDetailViewModel> GetAllDetailByAdmin(Guid id, string include = null)
        {

            var currentUserID = new Guid(_orderDetaiRrepository.GetCurrentUserId());
            Expression<Func<OrderDetail, bool>> filter = _ => _.OrderId == id;
            return GetOrderDetail(id,filter, include);
        }

        public BaseViewModel<OrderWithOrderDetailViewModel> GetAllDetailByUser(Guid id, string include = null)
        {

            var currentUser = _orderDetaiRrepository.GetUsername();
            Expression<Func<OrderDetail, bool>> filter = _ => _.OrderId == id && _.CreatedBy == currentUser;
            return GetOrderDetail(id,filter, include);
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
                var listDetail = _orderDetaiRrepository.GetAll().Where(x => x.OrderId == id);

                foreach (var detail in listDetail)
                {
                    _orderDetaiRrepository.Delete(detail);
                }
                var entities = _mapper.Map<IEnumerable<OrderDetail>>(details);
                foreach (var entity in entities)
                {
                    entity.Id = Guid.NewGuid();
                    entity.SetDefaultInsertValue(_orderDetaiRrepository.GetUsername());
                    _orderDetaiRrepository.Add(entity);
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
