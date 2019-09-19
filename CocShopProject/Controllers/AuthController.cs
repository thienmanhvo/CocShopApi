using CocShop.Data.Appsettings;
using CocShop.Data.Entity;
using CocShopProject.VIewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CocShopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Field

        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<MyRole> _roleManager;

        #endregion

        #region Ctor

        public AuthController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MyUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<MyRole>>(); ;
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
        public async Task<ActionResult> GetToken([FromBody]LoginVM request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return BadRequest("Invalid Username");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return BadRequest("Invalid Password");
            }

            await _userManager.UpdateAsync(user);
            return new OkObjectResult(GenerateToken(user).Result);
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
        public async Task<ActionResult> Register([FromBody]RegisterVM request)
        {
            var user = new MyUser()
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.Fullname,
            };
            await CreateRole();
            var resultUser = await _userManager.CreateAsync(user, request.Password);
            IdentityResult resultRole;
            if (resultUser.Succeeded)
            {
                resultRole = await _userManager.AddToRoleAsync(user, AppSettings.Configs.GetValue<string>("Role:User"));
                if (resultRole.Succeeded)
                {
                    return new OkObjectResult(GenerateToken(user).Result);
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

        private async Task<Token> GenerateToken(MyUser user)
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

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //create token
            var token = new JwtSecurityToken(
                    issuer: AppSettings.Configs.GetValue<string>("JwtSettings:Issuer"),
                    audience: user.FullName,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCredentials,
                    claims: claims
                );
            //return token
            return new Token
            {
                roles = _userManager.GetRolesAsync(user).Result.ToArray(),
                fullname = user.FullName,
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = (int)TimeSpan.FromDays(1).TotalSeconds
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
                    //create the roles and seed them to the database: Question 1
                    roleResult = await _roleManager.CreateAsync(new MyRole() { Name = roleName });
                }
            }
        }
    }
}