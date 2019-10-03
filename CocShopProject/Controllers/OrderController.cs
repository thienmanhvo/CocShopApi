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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IOrderService _orderService;

        #endregion

        public OrdersController(IServiceProvider serviceProvider)
        {
            _orderService = serviceProvider.GetRequiredService<IOrderService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/Product
        [HttpGet]
        public ActionResult<BaseViewModel<IEnumerable<ProductViewModel>>> GetOrder()
        {
            var result = _orderService.GetAllOrders();

            HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> GetProduct(string id)
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
            var result = _orderService.GetOrder(guidId);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // PUT: api/Product/5
        [ValidateModel]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> PutProduct(string id, [FromBody]UpdateProductRequestViewModel product)
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
            var result = _orderService.UpdateOrder(guidId, product);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // POST: api/Product
        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<ProductViewModel>> PostProduct(CreateProductRequestViewModel product)
        {
            var result = _orderService.CreateOrder(product);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<string>> DeleteProduct(string id)
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
            var result = _orderService.DeleteOrder(guidId);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Product.Any(e => e.Id == id);
        //}
    }
}
