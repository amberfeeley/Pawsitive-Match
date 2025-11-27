using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PawsitiveMatch.Authentication;
using PawsitiveMatch.SharedModels.Models;

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
            var dbUser = await _auth.LoginUserAsync(loginRequest.Email, loginRequest.Password);
            if (dbUser == null)
            {
                return Unauthorized();
            }
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.Email),
                new Claim("firstName", dbUser.FirstName),
                new Claim("lastName", dbUser.LastName),
            };

            var identity = new ClaimsIdentity(claims, "PawsitiveAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("PawsitiveAuth", principal);

            dbUser.Password = string.Empty;
            return Ok(dbUser);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("PawsitiveAuth");
            return Ok();
        }
    }
}