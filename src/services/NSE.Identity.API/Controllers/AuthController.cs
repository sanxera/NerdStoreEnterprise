using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Core.Messages.Integration;
using NSE.Identity.API.Models;
using NSE.MessageBus;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identity;

namespace NSE.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IMessageBus _bus;

        public AuthController(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings, IMessageBus bus)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _bus = bus;
            _appSettings = appSettings.Value;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var clientResult = await RegisterClient(userRegister);

                if (!clientResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);
                    return CustomResponse(clientResult.ValidationResult);
                }

                return CustomResponse(await GenerateJwt(userRegister.Email));
            }

            foreach (var error in result.Errors)
            {
                AddErrorProcessing(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _singInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorProcessing("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddErrorProcessing("Usuário ou senha incorretos");
            return CustomResponse();
        }

        private async Task<UserResponseLogin> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await this.GetUserClaims(claims, user);
            var encodedToken = EncodeToken(identityClaims);


            return GetResponseToken(encodedToken, user, claims);
        }

        private UserResponseLogin GetResponseToken(string encodedToken, IdentityUser user, IList<Claim> claims)
        {
            var response = new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHour).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHour),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<ResponseMessage> RegisterClient(UserRegister userRegister)
        {
            var user = await this._userManager.FindByEmailAsync(userRegister.Email);

            var userRegistered = new RegisteredUserIntegrationEvent(Guid.Parse(user.Id.ToString()), userRegister.Name, userRegister.Email, userRegister.Cpf);

            try
            {
                return await _bus.RequestAsync<RegisteredUserIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
