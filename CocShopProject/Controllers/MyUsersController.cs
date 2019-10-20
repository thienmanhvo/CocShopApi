using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CocShop.Core.Data.Entity;
using AutoMapper;
using CocShop.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using CocShop.Core.ViewModel;
using System.Net;
using CocShop.Core.Constaint;
using CocShop.Core.MessageHandler;
using Microsoft.AspNetCore.Authorization;
using CocShop.Core.Attribute;

namespace CocShop.WebAPi.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class MyUsersController : ControllerBase
    {

        #region Field

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMyUserService _productService;

        #endregion

        public MyUsersController(IServiceProvider serviceProvider)
        {
            _productService = serviceProvider.GetRequiredService<IMyUserService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        // GET: api/MyUser
        [HttpGet]
        public async Task<ActionResult<BaseViewModel<PagingResult<MyUserViewModel>>>> GetMyUser([FromQuery]BasePagingRequestViewModel request)
        {
            request.SetDefaultPage();
            //var a = request.Filters[0];

            var result = await _productService.GetAllMyUsers(request);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        // GET: api/MyUser/5
        [HttpGet("{id}")]
        public ActionResult<BaseViewModel<MyUserViewModel>> GetMyUser([CheckGuid]string id, string include = null)
        {

            var result = _productService.GetMyUser(new Guid(id), include);

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

    }
}
