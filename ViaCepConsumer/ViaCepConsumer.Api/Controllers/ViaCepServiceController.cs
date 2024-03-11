using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ViaCepConsumer.Api.Models;
using ViaCepConsumer.Api.Services.Interfaces;
using ViaCepConsumer.Api.ViewModels;

namespace ViaCepConsumer.Api.Controllers
{
    [ApiController]
    [Route("cep")]
    public class ViaCepServiceController : ControllerBase
    {
        private readonly IViaCepService _service;
        private readonly ICachingService _caching;

        public ViaCepServiceController(IViaCepService service, ICachingService caching)
        {
            _service = service;
            _caching = caching;
        }

        [HttpGet]
        [HttpPost]
        [Route("{cep}")]
        [Authorize]
        public async Task<IActionResult> Search([FromRoute] string cep)
        {
            string cepPattern = "[\\d]{5}-?[\\d]{3}";
            if (!Regex.IsMatch(cep, cepPattern))
                return StatusCode(400, new ResultViewModel<string>("Invalid CEP format. Please ensure that the data input follows either the format 00000-000 or 00000000."));

            string formattedCep = cep.Replace("-", string.Empty);
            ViaCepResponseModel response;
            
            try
            {
                var cacheResult = await _caching.Get(formattedCep);

                if (!string.IsNullOrEmpty(cacheResult))
                {
                    response = JsonConvert.DeserializeObject<ViaCepResponseModel>(cacheResult);
                    return StatusCode(200, new ResultViewModel<ViaCepResponseModel>(response, "These data were obtained and temporarily cached in-memory/storage. The cached data will expire after 5 minutes from the initial data entry request, facilitating subsequent queries within this timeframe."));
                }

                response = await _service.Search(formattedCep);
                await _caching.Set(formattedCep, JsonConvert.SerializeObject(response));

                return StatusCode(200, new ResultViewModel<ViaCepResponseModel>(response, "These data were obtained from ViaCEP Web Service (http://viacep.com.br). For the next 5 minutes, these data will be stored in-memory/cache storage, facilitating future queries with this same data entry."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
