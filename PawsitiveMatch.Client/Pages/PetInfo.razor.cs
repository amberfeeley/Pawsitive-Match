using Microsoft.AspNetCore.Components;
using MudBlazor;
using PawsitiveMatch.Services;
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
        private async Task CheckoutPet()
        {
            if (State.CurrentUser == null)
            {
                Snackbar.Add("You must log in to adopt a pet. Please log in first before making an adoption request.", Severity.Error);
            }
            else
            {
                Snackbar.Add("Your adoption request has been placed into the cart!", Severity.Success);
            }
        }
    }
}