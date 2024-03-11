using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [Route("{cep}")]
        [Authorize]
        public async Task<IActionResult> Search([FromRoute] string cep)
        {
            ViaCepResponseModel response;
            
            try
            {
                var cacheResult = await _caching.Get(cep);

                if (!string.IsNullOrEmpty(cacheResult))
                {
                    response = JsonConvert.DeserializeObject<ViaCepResponseModel>(cacheResult);
                    return StatusCode(200, new ResultViewModel<ViaCepResponseModel>(response, "This data was obtained from in-memory storage (Redis)."));
                }

                response = await _service.Search(cep);
                await _caching.Set(cep, JsonConvert.SerializeObject(response));

                return StatusCode(200, new ResultViewModel<ViaCepResponseModel>(response, "This data was obtained from ViaCEP Web Service."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
