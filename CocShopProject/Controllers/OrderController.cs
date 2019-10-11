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
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace CocShop.WebAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IOrderService _orderService;

        #endregion

        public OrderController(IServiceProvider serviceProvider)
        {
            _orderService = serviceProvider.GetRequiredService<IOrderService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<OrderViewModel>>>> GetOrder([FromQuery]BasePagingRequestViewModel request)
        {
            var listRole = HttpContext.User.FindAll(ClaimTypes.Role);
            request.SetDefaultPage();
            BaseViewModel<PagingResult<OrderViewModel>> result = null;
            if (listRole.Any(_ => Role.Admin.Equals(_.Value)))
            {
                result = await _orderService.GetAllOrdersByAdmin(request);
            }
            else
            {
                result = await _orderService.GetAllOrdersByUser(request);
            }

            HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<OrderViewModel>> GetOrder(string id)
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


        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<OrderViewModel>> PostOrder(CreateOrderRequestViewModel order)
        {


            var result = _orderService.CreateOrder(order);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

    }
}
