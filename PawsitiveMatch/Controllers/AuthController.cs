using Microsoft.AspNetCore.Mvc;
using PawsitiveMatch.Authentication;

namespace PawsitiveMatch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            bool success = await _auth.RegisterUserAsync(dto.Email, dto.Password, dto.FirstName, dto.LastName);
            return success ? Ok() : Conflict("Failing in registration");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var user = await _auth.LoginUserAsync(dto.Email, dto.Password);
            return user != null ? Ok(user) : Unauthorized();
        }
    }

    public record RegisterUserDto(string Email, string Password, string FirstName, string LastName);
    public record UserDto(string Email, string Password);
}