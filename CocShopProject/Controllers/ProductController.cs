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
using CocShop.Core.Attribute;

namespace CocShop.WebAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        #endregion

        public ProductsController(IServiceProvider serviceProvider)
        {
            _productService = serviceProvider.GetRequiredService<IProductService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<ProductViewModel>>>> GetProduct([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _productService.GetAllProducts(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/Product
        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<ProductViewModel>>>> GetAllProduct([FromQuery]BaseRequestViewModel request)
        {
            //var a = request.Filters[0];

            var result = await _productService.GetAllProductsNoPaging(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> GetProduct([CheckGuid]string id, [FromQuery] string include)
        {
            var result = _productService.GetProduct(new Guid(id), include);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // PUT: api/Product/5
        [Authorize(Roles = Role.Admin)]
        [ValidateModel]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> PutProduct([CheckGuid(Property = "LocationId")]string id, [FromBody]UpdateProductRequestViewModel product)
        {
            var guidID = new Guid(id);
            product.Id = guidID;
            var result = _productService.UpdateProduct(guidID, product);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // POST: api/Product
        [Authorize(Roles = Role.Admin)]
        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<ProductViewModel>> PostProduct(CreateProductRequestViewModel product)
        {
            var result = _productService.CreateProduct(product);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // DELETE: api/Product/5
        [Authorize(Roles = Role.Admin)]
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
            var result = _productService.DeleteProduct(guidId);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Product.Any(e => e.Id == id);
        //}
    }
}
