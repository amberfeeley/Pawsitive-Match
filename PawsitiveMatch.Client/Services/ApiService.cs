using System.Net.Http.Json;
using PawsitiveMatch.Client.Dto;

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
                var user = new RegisterUserDto(email, password, firstName, lastName);
                var response = await _http.PostAsJsonAsync("api/auth/register", user);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"RegisterAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<UserDto>()
                : null;
        }
    }
}