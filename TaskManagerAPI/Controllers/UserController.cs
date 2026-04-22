using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
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

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUsers), new { id = result.Id }, result);
        }

        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var token = _userService.Login(loginUser);
            if (token == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(new { token });
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}