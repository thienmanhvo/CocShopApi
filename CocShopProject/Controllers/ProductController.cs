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
        public ActionResult<BaseViewModel<IEnumerable<ProductViewModel>>> GetProduct()
        {
            return Ok(_productService.GetAllProducts());
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> GetProduct(string id)
        {
            return Ok(_productService.GetProduct(new Guid(id)));
        }

        // PUT: api/Product/5
        [ValidateModel]
        [HttpPut("{id}")]
        public ActionResult<BaseViewModel<ProductViewModel>> PutProduct(string id, [FromBody]UpdateProductRequestViewModel product)
        {
            return Ok(_productService.UpdateProduct(id, product));
        }

        // POST: api/Product
        [ValidateModel]
        [HttpPost]
        public ActionResult<BaseViewModel<ProductViewModel>> PostProduct(CreateProductRequestViewModel product)
        {
            return Ok(_productService.CreateProduct(product));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<string>> DeleteProduct(string id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Product.Any(e => e.Id == id);
        //}
    }
}
