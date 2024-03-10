using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Contexts;
using ViaCepConsumer.Api.Models;

namespace ViaCepConsumer.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public UserController(DatabaseContext dbContext)
            => _dbContext = dbContext;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            if (!model.IsValid())
                return StatusCode(400, "Passwords don't match!");

            User user = new(model);

            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return StatusCode(201, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            try
            {
                User? user = await _dbContext.Users
                                             .FirstOrDefaultAsync(x => string.Equals(x.Username, model.Username));

                if (user is null || !string.Equals(user.Password, model.Password))
                    return StatusCode(401, "Username or password is incorrect.");

                return StatusCode(200, user);
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
