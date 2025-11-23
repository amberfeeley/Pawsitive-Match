using System.Net.Http.Json;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Services
{
    public class PetService
    {
        private readonly HttpClient _http;

        public PetService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Pet>> GetPetsByTypeAsync(PetType type)
        {
            try
            {
                var pets = await _http.GetFromJsonAsync<List<Pet>>($"api/pets/{type}");
                return pets ?? new List<Pet>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"GetPetsByTypeAsync failed: {ex.Message}");
                return new List<Pet>();
            }
        }

        public async Task<Pet> GetPetByIDAndTypeAsync(int id, string type)
        {
            try
            {
                var pets = await _http.GetFromJsonAsync<List<Pet>>($"api/pets/{type}");
                var Pet = pets?.FirstOrDefault(p => p.Id == id);
                return Pet ?? new Pet();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"GetPetByIdAsync failed. {ex.Message}");
                return new Pet();
            }
        }
    }
}