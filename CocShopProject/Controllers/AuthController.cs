using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.MessageHandler;
using CocShop.Core.Service;
using CocShop.Core.ViewModel;
using CocShop.Data.Appsettings;
using CocShop.Service.Helpers;
using CocShop.WebAPi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CocShop.WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Field

        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<MyRole> _roleManager;
        private readonly IMyUserService _myUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        #endregion

        #region Ctor

        public AuthController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MyUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<MyRole>>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _myUserService = serviceProvider.GetRequiredService<IMyUserService>();
        }

        #endregion

        #region Login

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [ValidateModel]
        [HttpPost("Login")]
        public async Task<ActionResult<BaseViewModel<TokenViewModel>>> Login([FromBody]LoginViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = ErrMessageConstants.INVALID_USERNAME,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_USERNAME),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = ErrMessageConstants.INVALID_PASSWORD,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_PASSWORD),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            //await _userManager.UpdateAsync(user);
            return Ok(new BaseViewModel<TokenViewModel>
            {
                Data = GenerateToken(user).Result,
            });
        }

        #endregion

        #region Login

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>   
        [Authorize]
        [ValidateModel]
        [HttpGet("GetToken")]
        public async Task<ActionResult<BaseViewModel<TokenViewModel>>> GetToken()
        {
            var userId = GetCurrentUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = ErrMessageConstants.INVALID_USERNAME,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALID_USERNAME),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            // await _userManager.UpdateAsync(user);
            return Ok(new BaseViewModel<TokenViewModel>
            {
                Data = GenerateToken(user).Result,
            });
        }

        #endregion

        #region Register

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [ValidateModel]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]RegisterViewModel request)
        {
            var user = _mapper.Map<MyUser>(request);
            await CreateRole();
            user.SetDefaultInsertValue(user.UserName);
            var resultUser = await _userManager.CreateAsync(user, request.Password);
            IdentityResult resultRole;
            if (resultUser.Succeeded)
            {
                resultRole = await _userManager.AddToRoleAsync(user, AppSettings.Configs.GetValue<string>("Role:User"));
                if (resultRole.Succeeded)
                {
                    return Ok(new BaseViewModel<TokenViewModel>(GenerateToken(user).Result));
                }
                else
                {

                    return BadRequest(new BaseViewModel<TokenViewModel>
                    {
                        Code = resultRole.Errors.ElementAtOrDefault(0).Code,
                        Description = resultRole.Errors.ElementAtOrDefault(0).Description,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }
            }
            else
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = resultUser.Errors.ElementAtOrDefault(0).Code,
                    Description = resultUser.Errors.ElementAtOrDefault(0).Description,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        #endregion

        #region ChangePass

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [Authorize]
        [ValidateModel]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePasswordViewModel request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ChangePasswordAsync(currentUser, request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = result.Errors.ElementAtOrDefault(0).Code,
                    Description = result.Errors.ElementAtOrDefault(0).Description,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        #endregion

        #region UpdateInfo

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [Authorize]
        [ValidateModel]
        [HttpPut("UpdateInfo")]
        public async Task<ActionResult> UpdateInfo([FromBody]UpdateMyUserRequestViewModel request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            currentUser = _mapper.Map(request, currentUser);
            currentUser.SetDefaultUpdateValue(currentUser.UserName);
            IdentityResult result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = result.Errors.ElementAtOrDefault(0).Code,
                    Description = result.Errors.ElementAtOrDefault(0).Description,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }

        #endregion

        #region Profile

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [Authorize]
        [ValidateModel]
        [HttpGet("Profile")]
        public async Task<ActionResult<BaseViewModel<MyUserViewModel>>> Profile(string include)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = _myUserService.GetMyUser(new Guid(userId), include);// _mapper.Map<MyUserViewModel>();

            this.HttpContext.Response.StatusCode = (int)result.StatusCode;

            return result;
        }

        #endregion

        private async Task<TokenViewModel> GenerateToken(MyUser user)
        {

            var Key = BuildRsaSigningKey();

            //signing credentials
            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha512Signature);

            //add Claims
            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(Constants.CLAIM_USERNAME, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //create token
            var token = new JwtSecurityToken(
                    issuer: AppSettings.Configs.GetValue<string>("JwtSettings:Issuer"),
                    audience: user.FullName,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials,
                    claims: claims
                );
            //return token
            return new TokenViewModel
            {
                Roles = _userManager.GetRolesAsync(user).Result.ToArray(),
                Fullname = user.FullName,
                Email = user.Email,
                AvatarPath = user.AvatarPath,
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires_in = DateTime.Now.AddMinutes(30),

                //(int)TimeSpan.FromDays(1).TotalSeconds
            };
        }

        private RsaSecurityKey BuildRsaSigningKey()
        {
            var parameters = new RSAParameters()
            {
                Modulus = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:RsaModulus")),
                Exponent = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:RsaExponent")),
                P = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:P")),
                Q = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:Q")),
                DP = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:DP")),
                DQ = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:DQ")),
                InverseQ = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:InverseQ")),
                D = Base64UrlEncoder.DecodeBytes(AppSettings.Configs.GetValue<string>("JwtSettings:D")),
            };
            var rsaProvider = new RSACryptoServiceProvider(2048);
            rsaProvider.ImportParameters(parameters);
            var key = new RsaSecurityKey(rsaProvider);
            return key;
        }
        private async Task CreateRole()
        {
            var roleNames = AppSettings.Configs.GetSection("Role").Get<List<string>>();
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database:
                    MyRole role = new MyRole() { Name = roleName };
                    role.SetDefaultInsertValue(GetCurrentUser());
                    roleResult = await _roleManager.CreateAsync(role);
                }
            }
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
        private string GetCurrentUserId()
        {
            //try
            //{
            return _accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //}
            //catch
            //{
            //    return "SYSTEM";
            //}
        }
    }
}