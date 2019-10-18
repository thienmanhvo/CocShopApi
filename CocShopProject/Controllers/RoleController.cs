using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CocShop.WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Field

        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<MyRole> _roleManager;
        private readonly IMyUserService _myUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        #endregion

        #region Ctor

        public RoleController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MyUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<MyRole>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _myUserService = serviceProvider.GetRequiredService<IMyUserService>();
        }

        #endregion

        #region CreateRole

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [Authorize(Roles = Role.Admin)]
        [ValidateModel]
        [HttpPost()]
        public async Task<ActionResult<BaseViewModel<MyUserRole>>> CreateRole(CreateRoleRequestViewModel request)
        {
            IdentityResult roleResult;

            var roleExist = await _roleManager.RoleExistsAsync(request.Name);
            if (!roleExist)
            {
                //create the roles and seed them to the database:
                MyRole role = new MyRole() { Name = request.Name};
                role.SetDefaultInsertValue(GetCurrentUser());
                roleResult = await _roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new BaseViewModel<string>()
                    {
                        Code = roleResult.Errors.ElementAtOrDefault(0).Code,
                        Description = roleResult.Errors.ElementAtOrDefault(0).Description,
                        StatusCode = HttpStatusCode.BadRequest
                    });

                }
            }
            else
            {
                return BadRequest(new BaseViewModel<string>()
                {
                    Code = "ROLE_EXISTED",
                    Description = "Role is existed.",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        #endregion

        // GET: api/Product
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public ActionResult<BaseViewModel<PagingResult<RoleViewModel>>> GetAll([FromQuery]BaseRequestViewModel request)
        {
            //var a = request.Filters[0];

            var data = _roleManager.Roles.ToList();
            var result = new BaseViewModel<PagingResult<RoleViewModel>>();
            result.Data = new PagingResult<RoleViewModel>
            {
                Results = _mapper.Map<IEnumerable<RoleViewModel>>(data),
                TotalRecords = data.Count,
            };

            return result;
        }

        private string GetCurrentUser()
        {
            //try
            //{
            return _accessor.HttpContext.User?.FindFirst("username")?.Value ?? "SYSTEM";
            //}
            //catch
            //{
            //    return "SYSTEM";
            //}
        }
    }
}