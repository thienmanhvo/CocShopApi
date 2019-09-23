using AutoMapper;
using CocShop.Core.ViewModel;
using CocShop.Data.Entity;
using CocShop.Service.Service;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategoriesController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductCategoryService _producCategorytService;

        #endregion


        #region ctor
        public ProductCategoriesController(DataContext context, IServiceProvider serviceProvider)
        {
            _producCategorytService = serviceProvider.GetRequiredService<IProductCategoryService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        #endregion

        // GET: api/ProductCategories
        [HttpGet]
        public  ActionResult<IEnumerable<ProductCategory>> GetProductCategory()
        {
            return Ok(_producCategorytService.GetProductCategories().ToList());
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public ActionResult<ProductCategory> GetProductCategory(string id)
        {
            var product = _producCategorytService.GetProductCategory(new Guid(id));

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //// PUT: api/ProductCategories/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProductCategory(Guid id, ProductCategory productCategory)
        //{
        //    if (id != productCategory.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(productCategory).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategories
        [ValidateModel]
        [HttpPost]
        public ActionResult<ProductCategoryViewModel> PostProductCategory(ProductCategoryCreateRequest request)
        {
            var entity = _producCategorytService.CreateProductCategory(request);
            _producCategorytService.Save();

            return Ok(_mapper.Map<ProductCategoryViewModel>(entity));
        }

        //// DELETE: api/ProductCategories/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ProductCategory>> DeleteProductCategory(Guid id)
        //{
        //    var productCategory = await _context.ProductCategory.FindAsync(id);
        //    if (productCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ProductCategory.Remove(productCategory);
        //    await _context.SaveChangesAsync();

        //    return productCategory;
        //}

        //private bool ProductCategoryExists(Guid id)
        //{
        //    return _context.ProductCategory.Any(e => e.Id == id);
        //}
    }
}
