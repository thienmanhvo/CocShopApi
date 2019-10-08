using AutoMapper;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using CocShop.Core.MessageHandler;
using CocShop.Core.Constaint;
using System.Net;

namespace CocShop.WebAPi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IOrderDetailService _orderDetailService;

        #endregion

        public OrderDetailController(IServiceProvider serviceProvider)
        {
            _orderDetailService = serviceProvider.GetRequiredService<IOrderDetailService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/OrderDetail/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<IEnumerable<OrderDetailViewModel>>> GetDetail(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return NotFound(new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = ErrMessageConstants.NOTFOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                });
            };
            var result = _orderDetailService.GetAllDetail(guidId);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // PUT: api/OrderDetail/5
        [ValidateModel]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<IEnumerable<OrderDetailViewModel>>> PutOrder(string id, [FromBody]IEnumerable<CreateOrderDetailViewModel> order)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return NotFound(new BaseViewModel<string>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = ErrMessageConstants.NOTFOUND,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.NOTFOUND),
                });
            };
            var result = _orderDetailService.UpdateDetail(guidId, order);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // POST: api/Order
        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<IEnumerable<OrderDetailViewModel>>> PostDetail(IEnumerable<CreateOrderDetailViewModel> order)
        {
            var result = _orderDetailService.CreateDetail(order);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }


        //private bool ProductExists(Guid id)
        //{
        //    return _context.Product.Any(e => e.Id == id);
        //}
    }
}
