using Application.Interfaces;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
    
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {

            var DTO = new LoginUserDto(username, password);
            var user = await _userService.LoginAsync(DTO);

            return Ok(user);    
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string username, string password)
        {

            var DTO = new RegisterUserDto(username, password);
            var user = await _userService.RegisterAsync(DTO);

            if (user == null)
            {
                return BadRequest("Error registering user.");
            }


            return Ok("Usuario se creo de manera satifactoria");
        }
    }
}