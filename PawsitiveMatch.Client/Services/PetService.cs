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
            return await _http.GetFromJsonAsync<List<Pet>>($"api/pets/{type}");
        }
    }
}