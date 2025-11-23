using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.SharedModels.Models;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    public static readonly List<Pet> Pets = new()
    {
        new Pet
        {
            Name = "Sherman",
            Type = PetType.Dog,
            Breed = "German Shepherd",
            ImagePath = "/images/pets/Sherman.jpeg",
            Description = "This loving senior is ready for a quiet retirement! At 13 years old, Sherman's favorite activities are sunning, sleeping, and relaxing."
        },
        new Pet
        {
            Name = "Akeela",
            Type = PetType.Dog,
            Breed = "German Shepherd",
            ImagePath = "/images/pets/Akeela.jpeg",
            Description = "This 5 year old pup needs an active home! He loves to play ball, hang out with his family, and chase squirrels."
        },
        new Pet
        {
            Name = "Fancy",
            Type = PetType.Cat,
            Breed = "Domestic Shorthair",
            ImagePath = "/images/pets/Fancy.jpeg",
            Description = "Fancy is a sweet girl who loves to show off her prissy side. She is used to the finer things in life, such as cuddling with her person and hanging out wherever you are."
        },
        new Pet
        {
            Name = "Turkey",
            Type = PetType.Cat,
            Breed = "Domestic Longhair",
            ImagePath = "/images/pets/Turkey.jpeg",
            Description = "Turkey is a sweet boy that is sure to squirm into the best places of your heart. He loves to make biscuits, sit on his person's lap, and play with the toys!"
        },
        new Pet
        {
            Name = "Papito",
            Type = PetType.Other,
            Breed = "Cockatiel",
            ImagePath = "/images/pets/Papito.jpeg",
            Description = "Papito is a fiercely loyal bird. He has a permanently broken wing which means he can't fly, but he will still find a way to fly into your heart."
        },
        new Pet
        {
            Name = "Hamster",
            Type = PetType.Other,
            Breed = "Hamster",
            ImagePath = "/images/pets/Hamster.jpg",
            Description = "This is a hamster. There is no ham. He does stir."
        },
    };

    private readonly AppDbContext _dbContext;

    public PetsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet("{type}")]
    public async Task<ActionResult<List<Pet>>> GetPetsByType(string type)
    {
        if (!Enum.TryParse<PetType>(type, true, out var petType))
        {
            return BadRequest("Invalid pet type");
        }

        var filteredPets = await _dbContext.Pet.Where(p => p.Type == petType).ToListAsync();

        if (!filteredPets.Any())
        {
            return NotFound("No pets of this type found");
        }

        return Ok(filteredPets);
    }
}
