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

        public async Task<List<Pet>?> GetPetsByTypeAsync(PetType type)
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

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            try
            {
                var pet = await _http.GetFromJsonAsync<Pet>($"api/pets/pet-{id}");
                return pet ?? new Pet();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"GetPetByIdAsync failed: {ex.Message}");
                return new Pet();
            }
        }

        public async Task<bool> UploadPetAsync(string name, PetType petType, string breed, string? description)
        {
            try
            {
                var pet = new Pet
                {
                    Name = name,
                    Type = petType,
                    Breed = breed,
                    Description = description
                };
                var response = await _http.PostAsJsonAsync("api/pets/upload-pet", pet);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"UploadPetAsync failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePetAsync(int id)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"api/pets/delete-pet", id);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"DeletePetAsync failed: {ex.Message}");
                return false;
            }
        }
    }
}