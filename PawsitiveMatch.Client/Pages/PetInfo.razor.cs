using Microsoft.AspNetCore.Components;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Pages
{
    public partial class PetInfo
    {
        [Parameter] public required string PetTypeString { get; set; }
        [Parameter] public required int PetId { get; set; }

        public Pet? Pet { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Pet = await PetService.GetPetByIDAndTypeAsync(PetId, PetTypeString);
        } 

        // Test class to ensure that the bind function works as intended.
        // Note that changes will be undone upon refreshing the page
        private void CheckoutPet()
        {
            if (Pet != null)
                Pet.Adopted = true;
        }
    }
}