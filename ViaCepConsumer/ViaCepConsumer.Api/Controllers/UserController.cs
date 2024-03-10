﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViaCepConsumer.Api.Entities;
using ViaCepConsumer.Api.Infrastructure.Repositories.Interfaces;
using ViaCepConsumer.Api.Models;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
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
                await _repository.Insert(user);
                return StatusCode(201, user);
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "Username or email already registered.");
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
                User? user = await _repository.Get(x => string.Equals(x.Username, model.Username));

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
                var users = await _repository.Get();

                return StatusCode(200, users);
            }
            catch
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
