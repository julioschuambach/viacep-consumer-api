using ViaCepConsumer.Api.Models;

namespace ViaCepConsumer.Api.Services.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponseModel> Search(string url);
    }
}
