using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Authentication
{
    public class PetsService
    {
        private readonly AppDbContext _db;

        public PetsService(AppDbContext db)
        {
            _db = db;
        }


        public async Task<List<Pet>?> GetPetsByTypeAsync(PetType type)
        {
            return await _db.Pet.Where(p => p.Type == type && p.Adopted == false).ToListAsync();
        }

        public async Task<Pet?> GetPetsByIdAsync(int id)
        {
            return await _db.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Adopted == false);
        }

        public async Task<bool> DeletePetAsync(int id)
        {
            var pet = await _db.Pet.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null || pet.Adopted)
            {
                return false;
            }

            _db.Pet.Remove(pet);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UploadPetAsync(string name, PetType petType, string breed, string? description)
        {
            if (await _db.Pet.AnyAsync(u => u.Name == name))
                return false;

            var uploadedPet = new Pet
            {
                Name = name,
                Type = petType,
                Breed = breed,
                Description = description,
                // TODO: upload actual photos
                ImagePath = "/images/sample.jpg"
            };

            _db.Pet.Add(uploadedPet);
            await _db.SaveChangesAsync();

            return true;
        }

    }
}