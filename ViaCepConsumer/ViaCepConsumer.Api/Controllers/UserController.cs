using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Contexts;
using ViaCepConsumer.Api.Models;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly ITokenService _tokenService;

        public UserController(DatabaseContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

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

                var token = _tokenService.GenerateToken(user);

                return StatusCode(200, token);
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<User> users = await _dbContext.Users
                                                   .Include(x => x.Roles)
                                                   .ToListAsync();

                return StatusCode(200, users);
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
