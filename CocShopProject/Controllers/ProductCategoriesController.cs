using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;

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
        public ActionResult<BaseViewModel<IEnumerable<ProductCategoryViewModel>>> GetProductCategory()
        {
            var result = _producCategorytService.GetAllProductCategories();
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<ProductCategoryViewModel>> GetProductCategory(string id)
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
            var result = _producCategorytService.GetProductCategory(guidId);
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
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
            var result = _producCategorytService.CreateProductCategory(request);
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;
            return result;
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public ActionResult<BaseViewModel<string>> DeleteProductCategory(string id)
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
            var result = _producCategorytService.DeleteProductCategory(guidId);
            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        //private bool ProductCategoryExists(Guid id)
        //{
        //    return _context.ProductCategory.Any(e => e.Id == id);
        //}
    }
}
