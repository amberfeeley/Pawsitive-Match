using Microsoft.AspNetCore.Components;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Pages
{
    public partial class AdoptablePets
    {
        [Parameter] public required string PetTypeString { get; set; }
        private PetType PetType;
        private List<Pet> Pets = [];

        protected override async Task OnParametersSetAsync()
        {
            if (!Enum.TryParse(PetTypeString, true, out PetType))
            {
                Nav.NavigateTo("/");
                return;
            }
            
            Pets = await PetService.GetPetsByTypeAsync(PetType);
        }
    }
}