using AutoMapper;
using CocShop.Core.Attribute;
using CocShop.Core.Constaint;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CocShop.WebAPi.Controllers
{
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IPaymentMethodService _locationService;

        #endregion


        public PaymentMethodsController(IServiceProvider serviceProvider)
        {
            _locationService = serviceProvider.GetRequiredService<IPaymentMethodService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<PaymentMethodViewModel>>>> GetPaymentMethod([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();

            var result = await _locationService.GetAllPaymentMethods(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<PaymentMethodViewModel>> GetPaymentMethod([CheckGuid(Property = "PaymentMethodId")]string id)
        {
            var result = _locationService.GetPaymentMethod(new Guid(id));

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // PUT: api/PaymentMethods/5
        [ValidateModel]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<PaymentMethodViewModel>> PutPaymentMethod([CheckGuid(Property = "PaymentMethodId")]string id, [FromBody]UpdatePaymentMethodRequestViewModel location)
        {
            var guidId = new Guid(id);
            location.Id = guidId;
            var result = _locationService.UpdatePaymentMethod(location);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // POST: api/PaymentMethods
        [Authorize(Roles = Role.User)]
        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<PaymentMethodViewModel>> PostPaymentMethod(CreatePaymentMethodRequestViewModel location)
        {
            var result = _locationService.CreatePaymentMethod(location);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<string>> DeletePaymentMethod([CheckGuid(Property = "PaymentMethodId")] string id)
        {
            var result = _locationService.DeletePaymentMethod(new Guid(id));

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }
    }
}
