using System.Net.Http.Json;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            try
            {
                var user = new User
                {
                    Email = email,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName
                };
                var response = await _http.PostAsJsonAsync("api/auth/register", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"RegisterAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequestDto
                {
                    Email = email,
                    Password = password
                };

                var response = await _http.PostAsJsonAsync("api/auth/login", loginRequest);

                return response.IsSuccessStatusCode
                    ? await response.Content.ReadFromJsonAsync<User>()
                    : null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"LoginAsync failed: {ex.Message}");
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _http.PostAsync("api/auth/logout", null);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"LogoutAsync failed: {ex.Message}");
            }
        }

        public async Task<List<Pet>?> GetCartAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/checkout");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<List<Pet>>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"GetCartAsync failed: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddPetToCartAsync(int petId)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/checkout/add/{petId}", petId);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"AddPetToCartAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemovePetFromCartAsync(int petId)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/checkout/remove/{petId}", petId);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"RemovePetFromCartAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AdoptPetsInCartAsync()
        {
            try
            {
                var response = await _http.PostAsync("api/checkout/adopt", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"AdoptPetsInCartAsync failed: {ex.Message}");
                return false;
            }
        }
    }

    internal class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}