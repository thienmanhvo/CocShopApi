using AutoMapper;
using CocShop.Data.Entity;
using CocShop.Service.Service;
using CocShop.Service.ViewModel;
using CocShopProject.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CocShopProject.Controllers
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
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            return Ok(_productService.GetProducts().ToList());
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(string id)
        {
            var product = _productService.GetProduct(new Guid(id));

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //// PUT: api/Product/5
        //[HttpPut("{id}")]
        //public IActionResult PutProduct(Guid id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Product
        [ValidateModel]
        [HttpPost]
        public ActionResult<ProductViewModel> PostProduct(ProductRequestViewModel product)
        {
            var entity = _productService.CreateProduct(product);
            _productService.Save();

            return Ok(_mapper.Map<ProductViewModel>(entity));
        }

        //// DELETE: api/Product/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Product>> DeleteProduct(string id)
        //{
        //    var product = await _context.Product.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Product.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return product;
        //}

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Product.Any(e => e.Id == id);
        //}
    }
}
