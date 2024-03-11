using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViaCepConsumer.Api.Services.Interfaces;

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
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
