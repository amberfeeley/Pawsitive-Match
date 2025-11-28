using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Authentication
{
    public class CheckoutService
    {
        private readonly AppDbContext _db;

        public CheckoutService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Pet>?> GetCartAsync(int userId)
        {
            return await _db.Pet.Where(u => u.InCartOfUserId == userId).ToListAsync();
        }

        public async Task<bool> AddPetToCartAsync(int userId, int petId)
        {
            var pet = await _db.Pet.FirstOrDefaultAsync(p => p.Id == petId);
            if (pet == null || pet.Adopted)
            {
                return false;
            }

            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            pet.InCartOfUserId = userId;
            user.CartPets.Add(pet);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePetFromCartAsync(int userId, int petId)
        {
            var pet = await _db.Pet.FirstOrDefaultAsync(p => p.Id == petId);
            if (pet == null)
            {
                return false;
            }

            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            pet.InCartOfUserId = null;
            user.CartPets.Remove(pet);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdoptPetsAsync(int userId)
        {
            var pets = await _db.Pet.Where(p => p.InCartOfUserId == userId).ToListAsync();

            if (!pets.Any())
            {
                return false;
            }

            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            foreach (var pet in pets)
            {
                pet.OwnerId = userId;
                pet.InCartOfUserId = null;
                pet.Adopted = true;
                user.AdoptedPets.Add(pet);
            }

            await _db.SaveChangesAsync();
            return true;
        }

    }
}