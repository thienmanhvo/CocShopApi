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
using System.Threading.Tasks;
using CocShop.Core.Data.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CocShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IServiceProvider serviceProvider)
        {
            _orderRepository = serviceProvider.GetRequiredService<IOrderRepository>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _productRepository = serviceProvider.GetRequiredService<IProductRepository>();
            _orderDetailRepository = serviceProvider.GetRequiredService<IOrderDetailRepository>();
            _locationRepository = serviceProvider.GetRequiredService<ILocationRepository>();
            _paymentMethodRepository = serviceProvider.GetRequiredService<IPaymentMethodRepository>();
            _storeRepository = serviceProvider.GetRequiredService<IStoreRepository>();

        }

        public BaseViewModel<OrderViewModel> CreateOrder(CreateOrderRequestViewModel order)
        {
            decimal totalPrice = 0;
            int totalQuantity = 0;
            var listOrderDetail = new HashSet<OrderDetail>();
            var username = _orderRepository.GetUsername();
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.SetDefaultInsertValue(_orderRepository.GetUsername());
            if (order.PaymentId != null)
            {
                var paymentMethod = _paymentMethodRepository.GetMany(_ => _.IsDelete == false && _.Id.Equals(new Guid(order.PaymentId)));
                if (paymentMethod == null)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.PAYMENT_METHOD_NOT_FOUND,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PAYMENT_METHOD_NOT_FOUND),
                    };
                }
            }
            else
            {
                orderEntity.IsCash = true;
            }
            var location = _locationRepository.GetMany(_ => _.IsDelete == false && _.Id.Equals(new Guid(order.LocationId)));
            if (location == null)
            {
                return new BaseViewModel<OrderViewModel>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = ErrMessageConstants.LOCATION_NOT_FOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.LOCATION_NOT_FOUND),
                };
            }

            foreach (var product in order.Products)
            {
                var productId = new Guid(product.Id);
                var productEntity = _productRepository.GetMany(_ => _.IsDelete == false && _.Id.Equals(productId)).FirstOrDefault();

                if (productEntity == null)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.PRODUCT_NOT_FOUND,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PRODUCT_NOT_FOUND),
                    };
                }
                if (productEntity?.IsSale ?? false)
                {
                    if (productEntity?.PriceSale != product?.Price)
                    {
                        return new BaseViewModel<OrderViewModel>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Code = ErrMessageConstants.PRODUCT_PRICE_NOT_FOUND,
                            Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PRODUCT_PRICE_NOT_FOUND),
                        };
                    }
                }
                else
                {
                    if (productEntity?.Price != product?.Price)
                    {
                        return new BaseViewModel<OrderViewModel>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Code = ErrMessageConstants.PRODUCT_PRICE_NOT_FOUND,
                            Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PRODUCT_PRICE_NOT_FOUND),
                        };
                    }
                }
                if (productEntity.Quantity < product.Quantity)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.OUT_OF_STOCK,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.OUT_OF_STOCK),
                    };
                }
                productEntity.Quantity = productEntity.Quantity - product.Quantity;
                _productRepository.Update(productEntity);

                var orderDetail = _mapper.Map<OrderDetail>(product);
                var totalDetailPrice = product.Price * product.Quantity;
                orderDetail.SetDefaultInsertValue(username);
                orderDetail.ProductId = productId;
                orderDetail.TotalPrice = totalDetailPrice;
                orderDetail.TotalPrice = totalDetailPrice;
                orderDetail.OrderId = orderEntity.Id;

                totalPrice += product.Price * product.Quantity;
                totalQuantity += product.Quantity;
                listOrderDetail.Add(orderDetail);
            }



            orderEntity.TotalPrice = totalPrice;
            orderEntity.TotalQuantity = totalQuantity;
            orderEntity.CreatedUserId = new Guid(_orderRepository.GetCurrentUserId());
            // orderEntity.OrderDetail = listOrderDetail;

            _orderRepository.Add(orderEntity);
            _orderDetailRepository.Add(listOrderDetail);


            var result = new BaseViewModel<OrderViewModel>()
            {
                Data = _mapper.Map<OrderViewModel>(orderEntity),
            };

            Save();

            return result;
        }

        public BaseViewModel<OrderViewModel> CreateOrderNoValidate(CreateOrderRequestViewModel order)
        {
            decimal totalPrice = 0;
            int totalQuantity = 0;
            var listOrderDetail = new HashSet<OrderDetail>();
            var username = _orderRepository.GetUsername();
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.DeliveryToLatitude = order.Latitude;
            orderEntity.DeliveryToLongitude = order.longitude;

            orderEntity.SetDefaultInsertValue(_orderRepository.GetUsername());
            if (order.PaymentId != null)
            {
                var paymentMethod = _paymentMethodRepository.GetMany(_ => _.IsDelete == false && _.Id.Equals(new Guid(order.PaymentId)));
                if (paymentMethod == null)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.PAYMENT_METHOD_NOT_FOUND,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PAYMENT_METHOD_NOT_FOUND),
                    };
                }
            }
            else
            {
                orderEntity.IsCash = true;
            }

            foreach (var product in order.Products)
            {
                var productId = new Guid(product.Id);
                var productEntity = _productRepository.GetMany(_ => _.IsDelete == false && _.Id.Equals(productId)).FirstOrDefault();

                if (productEntity == null)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.PRODUCT_NOT_FOUND,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.PRODUCT_NOT_FOUND),
                    };
                }

                if (productEntity.Quantity < product.Quantity)
                {
                    return new BaseViewModel<OrderViewModel>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.OUT_OF_STOCK,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.OUT_OF_STOCK),
                    };
                }
                productEntity.Quantity = productEntity.Quantity - product.Quantity;
                _productRepository.Update(productEntity);

                var orderDetail = _mapper.Map<OrderDetail>(product);
                var totalDetailPrice = product.Price * product.Quantity;
                orderDetail.SetDefaultInsertValue(username);
                orderDetail.ProductId = productId;
                orderDetail.TotalPrice = totalDetailPrice;
                orderDetail.TotalPrice = totalDetailPrice;
                orderDetail.OrderId = orderEntity.Id;

                totalPrice += product.Price * product.Quantity;
                totalQuantity += product.Quantity;
                listOrderDetail.Add(orderDetail);
            }



            orderEntity.TotalPrice = totalPrice;
            orderEntity.TotalQuantity = totalQuantity;
            orderEntity.CreatedUserId = new Guid(_orderRepository.GetCurrentUserId());
            // orderEntity.OrderDetail = listOrderDetail;

            _orderRepository.Add(orderEntity);
            _orderDetailRepository.Add(listOrderDetail);


            var result = new BaseViewModel<OrderViewModel>()
            {
                Data = _mapper.Map<OrderViewModel>(orderEntity),
            };

            Save();

            return result;
        }

        private BaseViewModel<OrderViewModel> GetOrder(Expression<Func<Order, bool>> filter, string include = null)
        {
            var includeList = IncludeLinqHelper<Order>.StringToListInclude(include);

            var order = _orderRepository.Get(filter, includeList).FirstOrDefault();

            if (order == null)
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
        public BaseViewModel<OrderViewModel> GetOrderByAdmin(Guid id, string include = null)
        {
            var currentUserID = new Guid(_orderRepository.GetCurrentUserId());
            Expression<Func<Order, bool>> filter = _ => _.Id == id;
            return GetOrder(filter, include);
        }
        public BaseViewModel<OrderViewModel> GetOrderByUser(Guid id, string include = null)
        {
            var currentUserID = new Guid(_orderRepository.GetCurrentUserId());
            Expression<Func<Order, bool>> filter = _ => _.Id == id && _.CreatedUserId == currentUserID;
            return GetOrder(filter, include);
        }

        public async Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrdersByUser(BasePagingRequestViewModel request)
        {
            var currentUser = _orderRepository.GetUsername();
            return await GetAllOrder(request, $"_.CreatedBy == \"{currentUser}\"");

        }
        private async Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrder(BasePagingRequestViewModel request, string defaultCondition = null)
        {
            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var result = new BaseViewModel<PagingResult<OrderViewModel>>();
            string filter = SearchHelper<Order>.GenerateStringExpression(request.Filter, defaultCondition);

            Expression<Func<Order, bool>> FilterExpression = await LinqHelper<Order>.StringToExpression(filter);

            var includeList = IncludeLinqHelper<Order>.StringToListInclude(request?.Include);

            QueryArgs<Order> queryArgs = new QueryArgs<Order>
            {
                Offset = pageSize * (pageIndex - 1),
                Limit = pageSize,
                Filter = FilterExpression,
                Sort = request.SortBy,
                Include = includeList
            };

            var data = _orderRepository.Get(queryArgs.Filter, queryArgs.Sort, queryArgs.Offset, queryArgs.Limit, queryArgs.Include).ToList();

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
                    TotalRecords = _orderRepository.Count(queryArgs.Filter)
                };
                foreach (var item in result.Data.Results)
                {
                    if (item.StoreId != null) { item.Store = _mapper.Map<StoreViewModel>(_storeRepository.GetMany(_ => _.Id == new Guid(item.StoreId)).FirstOrDefault()); }
                }
            }

            return result;
        }
        public async Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrdersByAdmin(BasePagingRequestViewModel request)
        {
            return await GetAllOrder(request);
        }
        public async Task<BaseViewModel<PagingResult<OrderViewModel>>> GetAllOrdersByAdminNoPaging(BaseRequestViewModel request)
        {
            return await GetAllOrder(new BasePagingRequestViewModel
            {
                Filter = request.Filter,
                Include = request.Include,
                SortBy = request.SortBy
            });
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public BaseViewModel<string> CancelOrder(Guid id)
        {
            var currentUserId = new Guid(_orderRepository.GetCurrentUserId());
            var entity = _orderRepository.GetMany(_ => _.Id == id && _.CreatedUserId == currentUserId).Include(_ => _.OrderDetail).FirstOrDefault();
            BaseViewModel<string> result = null;

            if (entity == null)
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
                if (entity.Status != MyEnum.OrderStatus.Submitted.ToString())
                {
                    result = new BaseViewModel<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.INVALID_ORDER_STATUS_SUBMITTED,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_ORDER_STATUS_SUBMITTED)
                    };
                }
                else
                {
                    entity.Status = MyEnum.OrderStatus.Canceled.ToString();

                    entity.SetDefaultUpdateValue(_orderRepository.GetUsername());
                    _orderRepository.Update(entity);
                    foreach (var item in entity.OrderDetail)
                    {
                        var product = _productRepository.GetById(item.ProductId);
                        product.Quantity = item.Quantity + product.Quantity;
                        _productRepository.Update(product);
                    }

                    result = new BaseViewModel<string>();

                    Save();
                }

            }
            return result;
        }

        public BaseViewModel<string> PickOrder(Guid id)
        {
            var currentUserId = new Guid(_orderRepository.GetCurrentUserId());
            var entity = _orderRepository.Get(_ => _.Id == id);
            BaseViewModel<string> result = null;

            if (entity == null)
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
                if (entity.Status != MyEnum.OrderStatus.Submitted.ToString())
                {
                    result = new BaseViewModel<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.INVALID_ORDER_STATUS_SUBMITTED,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_ORDER_STATUS_SUBMITTED)
                    };
                }
                else
                {
                    entity.Status = MyEnum.OrderStatus.Delivering.ToString();
                    entity.DeliveryUserId = currentUserId;

                    entity.SetDefaultUpdateValue(_orderRepository.GetUsername());
                    _orderRepository.Update(entity);
                    result = new BaseViewModel<string>();
                }

                Save();
            }

            return result;
        }

        public BaseViewModel<string> CompleteOrder(Guid id)
        {
            var currentUserId = new Guid(_orderRepository.GetCurrentUserId());
            var entity = _orderRepository.Get(_ => _.Id == id && _.DeliveryUserId == currentUserId);
            BaseViewModel<string> result = null;

            if (entity == null)
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
                if (entity.Status != MyEnum.OrderStatus.Delivering.ToString())
                {
                    result = new BaseViewModel<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = ErrMessageConstants.INVALID_ORDER_STATUS_DELIVERING,
                        Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_ORDER_STATUS_DELIVERING)
                    };
                }
                else
                {
                    entity.Status = MyEnum.OrderStatus.Delivered.ToString();

                    entity.SetDefaultUpdateValue(_orderRepository.GetUsername());
                    _orderRepository.Update(entity);
                    result = new BaseViewModel<string>();
                    Save();
                }
            }

            return result;
        }

    }
}
