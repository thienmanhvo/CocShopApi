using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CocShop.WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBrandService _brandService;

        #endregion

        public BrandsController(IServiceProvider serviceProvider)
        {
            _brandService = serviceProvider.GetRequiredService<IBrandService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<BrandViewModel>>>> GetAllStore([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _brandService.GetAllBrands(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }
    }
}