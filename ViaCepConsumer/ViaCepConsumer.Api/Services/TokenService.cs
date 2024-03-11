using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Extensions;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
            => _configuration = configuration;

        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtKey").Value);
            var claims = user.GetClaims();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(
                                                new SymmetricSecurityKey(key),
                                                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
