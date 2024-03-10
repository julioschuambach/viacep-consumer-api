using System.Security.Principal;
using ViaCepConsumer.Api.Entities;

namespace ViaCepConsumer.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
