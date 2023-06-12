using BlazorShop.Domain.Commands.Authentications;
using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Handlers.Authentications;
using BlazorShop.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorShop.Api.Controllers
{
    [Route("v1/authentications")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("signin/email")] // apoli@gmail.com - apoli1234
        [HttpPost]
        public GenericCommandResult SignInEmail(LoginEmailCommand command, [FromServices] LoginEmailHandle handle)
        {
            var result = (GenericCommandResult)handle.Handler(command);

            if (result.SuccessFailure)
            {
                var token = GenerateJSONWebToken((User)result.Data);

                return new GenericCommandResult(true, "User successfully logged in!", new { token = token });
            }

            return new GenericCommandResult(false, result.Message, result.Data);
        }



        [Route("signin/userName")]
        [HttpPost]
        public GenericCommandResult SignInUserName(LoginUserNameCommand command, [FromServices] LoginUserNameHandle handle)
        {
            var result = (GenericCommandResult)handle.Handler(command);

            if (result.SuccessFailure)
            {
                var token = GenerateJSONWebToken((User)result.Data);

                return new GenericCommandResult(true, "User successfully logged in!", new { token = token });
            }

            return new GenericCommandResult(false, result.Message, result.Data);
        }



        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("blazorShop-authentication-key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(ClaimTypes.Role, userInfo.UserType.ToString()),
            new Claim("role", userInfo.UserType.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            // configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken(
                "blazorShop",
                "blazorShop",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
