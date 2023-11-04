using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MovieHub.Utilities.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _securityKey;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationService(
            IConfiguration configuration, 
            IHttpContextAccessor contextAccessor)
        {
            _securityKey = configuration.GetSection("SecurityToken").Value!;
            _contextAccessor = contextAccessor;
        }

        public string IssueJwtToken(string id, string firstName, string lastName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, $"{firstName} {lastName}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ClaimsIdentity> ValidateTokenAsync(HttpContext context, string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_securityKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };

                var principal = await tokenHandler.ValidateTokenAsync(token, validationParameters);
                context.Items["User"] = principal;

                return principal.ClaimsIdentity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AddToHeaders(string token)
        {
            _contextAccessor.HttpContext.Request.Headers.Add("Authorization", "Bearer " + token);
        }
    }
}
