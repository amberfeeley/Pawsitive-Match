using System.Net.Http.Json;
using PawsitiveMatch.SharedModels;

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
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"RegisterAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };
            var response = await _http.PostAsJsonAsync("api/auth/login", user);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<User>()
                : null;
        }
    }
}