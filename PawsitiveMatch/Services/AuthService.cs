using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Authentication
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<User> _hasher = new();
        public User Admin { get; private set; }
        
        public AuthService(AppDbContext db)
        {
            _db = db;

            Admin = new User
            {
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                Role = "Admin"
            };

            Admin.Password = _hasher.HashPassword(Admin, "Admin123!");
        }

        public async Task<bool> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            if (await _db.User.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                Email = email,
                Password = "",
                FirstName = firstName,
                LastName = lastName,
                AdoptedPets = [],
                CartPets = []
            };
            user.Password = _hasher.HashPassword(user, password);

            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            var user = await _db.User.Include(u => u.AdoptedPets).Include(u => u.CartPets).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }

            var result = _hasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
