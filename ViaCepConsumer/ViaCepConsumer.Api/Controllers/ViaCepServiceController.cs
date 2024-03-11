using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ViaCepServiceController(IViaCepService service)
            => _service = service;

        [HttpGet]
        [Route("{cep}")]
        [Authorize]
        public async Task<IActionResult> Search([FromRoute] string cep)
        {
            try
            {
                var response = await _service.Search(cep);
                return StatusCode(200, new ResultViewModel<ViaCepResponseModel>(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<string>(ex.Message));
            }
        }
    }
}
