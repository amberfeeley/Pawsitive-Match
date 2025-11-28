using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitiveMatch.Authentication;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkout;

        public CheckoutController(CheckoutService checkout)
        {
            _checkout = checkout;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPost("add/{petId}")]
        public async Task<IActionResult> AddPetToCart([FromBody] int petId)
        {
            int userId = GetUserId();
            bool success = await _checkout.AddPetToCartAsync(userId, petId);
            return success ? Ok("Pet added to cart") : BadRequest("Cannot add pet to cart");
        }

        [HttpPost("remove/{petId}")]
        public async Task<IActionResult> RemovePetFromCart([FromBody] int petId)
        {
            int userId = GetUserId();
            bool success = await _checkout.RemovePetFromCartAsync(userId, petId);
            return success ? Ok("Pet removed from cart") : BadRequest("Cannot remove pet from cart");
        }

        [HttpPost("adopt")]
        public async Task<IActionResult> AdoptPets()
        {
            int userId = GetUserId();
            bool success = await _checkout.AdoptPetsAsync(userId);
            return success ? Ok("Pets adopted successfully") : BadRequest("Unable to adopt pets");
        }
    }
}