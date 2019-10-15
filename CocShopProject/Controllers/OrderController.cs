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
using CocShop.Core.Attribute;

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
        public ActionResult<BaseViewModel<OrderViewModel>> GetOrder([CheckGuid]string id, [FromQuery] string include)
        {
            var listRole = HttpContext.User.FindAll(ClaimTypes.Role);
            BaseViewModel<OrderViewModel> result = null;
            if (listRole.Any(_ => Role.Admin.Equals(_.Value)))
            {
                result = _orderService.GetOrderByAdmin(new Guid(id), include);
            }
            else
            {
                result = _orderService.GetOrderByUser(new Guid(id), include);
            }
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // PUT: api/Order/Cancel/5
        [Authorize(Roles = Role.User)]
        [HttpPut("Cancel/{id}")]
        public ActionResult<BaseViewModel<string>> Cancel([CheckGuid]string id)
        {
            var listRole = HttpContext.User.FindAll(ClaimTypes.Role);
            BaseViewModel<string> result = null;
            result = _orderService.CancelOrder(new Guid(id));
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }
        
        // PUT: api/Order/Pick/5
        [Authorize(Roles = Role.Staff)]
        [HttpPut("Pick/{id}")]
        public ActionResult<BaseViewModel<string>> Pick([CheckGuid]string id)
        {
            var listRole = HttpContext.User.FindAll(ClaimTypes.Role);
            BaseViewModel<string> result = null;
            result = _orderService.PickOrder(new Guid(id));
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }
        
        // PUT: api/Order/Complete/5
        [Authorize(Roles = Role.Staff)]
        [HttpPut("Complete/{id}")]
        public ActionResult<BaseViewModel<string>> Complete([CheckGuid]string id)
        {
            var listRole = HttpContext.User.FindAll(ClaimTypes.Role);
            BaseViewModel<string> result = null;
            result = _orderService.CompleteOrder(new Guid(id));
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }


        [ValidateModel]
        [Authorize(Roles = Role.User)]
        [HttpPost]
        public ActionResult<BaseViewModel<OrderViewModel>> PostOrder(CreateOrderRequestViewModel order)
        {


            var result = _orderService.CreateOrder(order);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

    }
}
