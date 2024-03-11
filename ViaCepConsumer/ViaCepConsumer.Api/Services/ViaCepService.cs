using ViaCepConsumer.Api.Models;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly IConfiguration _configuration;

        public ViaCepService(IConfiguration configuration)
            => _configuration = configuration;

        public async Task<ViaCepResponseModel> Search(string cep)
        {
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(
                string.Format(_configuration.GetSection("ViaCepUrl").Value, cep));

            return await response.Content.ReadAsAsync<ViaCepResponseModel>();
        }
    }
}
