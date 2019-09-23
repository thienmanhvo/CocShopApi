using AutoMapper;
using CocShop.Core.Data.Entity;
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
        public ActionResult<BaseViewModel<IEnumerable<ProductCategory>>> GetProductCategory()
        {
            return Ok(_producCategorytService.GetAllProductCategories());
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<ProductCategory>> GetProductCategory(string id)
        {
            return Ok(_producCategorytService.GetProductCategory(id));
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
        public ActionResult<BaseViewModel<ProductCategoryViewModel>> PostProductCategory(ProductCategoryCreateRequest request)
        {
            return Ok(_producCategorytService.CreateProductCategory(request));
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public ActionResult<ProductCategory> DeleteProductCategory(string id)
        {
            return Ok(_producCategorytService.DeleteProductCategory(id));
        }

        //private bool ProductCategoryExists(Guid id)
        //{
        //    return _context.ProductCategory.Any(e => e.Id == id);
        //}
    }
}
