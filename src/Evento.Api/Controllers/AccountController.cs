using System;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }
        [HttpGet("tickets")]
        public Task<IActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(),
                    command.Email,command.Name,command.Password,command.Role);

            return Created("/account",null);
        }
        
        [HttpPost("login")]
        public Task<IActionResult> Login(Login command)
        {
            throw new NotImplementedException();
        }

    }
}