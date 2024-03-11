using System.Security.Claims;
using ViaCepConsumer.Api.Entities;

namespace ViaCepConsumer.Api.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims;
        }
    }
}
