using AutoMapper;
using CocShop.Core.Constaint;
using CocShop.Core.Data.Entity;
using CocShop.Core.MessageHandler;
using CocShop.Core.ViewModel;
using CocShop.Data.Appsettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        }

        #endregion

        #region Login

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <author>thiennb</author>
        [HttpPost("Login")]
        public async Task<ActionResult<BaseViewModel<TokenViewModel>>> GetToken([FromBody]LoginViewModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = ErrMessageConstants.INVALIDUSERNAME,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALIDUSERNAME),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return BadRequest(new BaseViewModel<TokenViewModel>
                {
                    Code = ErrMessageConstants.INVALIDPASSWORD,
                    Description = MessageHandler.CustomErrMessage(ErrMessageConstants.INVALIDPASSWORD),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            await _userManager.UpdateAsync(user);
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
                    return BadRequest(resultRole.Errors);
                }
            }
            else
            {
                return BadRequest(resultUser.Errors);
            }
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
                roles = _userManager.GetRolesAsync(user).Result.ToArray(),
                fullname = user.FullName,
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = DateTime.Now.AddMinutes(30),

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
    }
}