using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PawsitiveMatch.Authentication;
using PawsitiveMatch.SharedModels;

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
        public async Task<IActionResult> Register([FromBody] User user)
        {
            bool success = await _auth.RegisterUserAsync(user.Email, user.Password, user.FirstName, user.LastName);
            return success ? Ok() : Conflict("Failing in registration");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            Console.WriteLine($"Attempting login for {loginRequest.Email}");
            var dbUser = await _auth.LoginUserAsync(loginRequest.Email, loginRequest.Password);
            if (dbUser == null)
            {
                Console.WriteLine("Login failed: invalid credentials");
                return Unauthorized();
            }

            Console.WriteLine("Login succeeded!");
            dbUser.Password = string.Empty;
            return Ok(dbUser);
        }
    }
}