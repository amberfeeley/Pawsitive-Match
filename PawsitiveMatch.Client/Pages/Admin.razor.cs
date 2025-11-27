using MudBlazor;
using PawsitiveMatch.Client.Services;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Admin
    {
        private string name = string.Empty;
        private PetType petType;
        private string breed = string.Empty;
        private string description = string.Empty;

        private bool success;
        private MudForm? form;

        protected override void OnInitialized()
        {
            if (!StateService.IsLoggedIn || StateService.CurrentUser?.Role != "Admin")
            {
                Nav.NavigateTo("/");
            }
        }

        private async Task UploadPet()
        {
            if (form != null)
            {
                await form.Validate();
                if (form.IsValid)
                {
                    await PetService.UploadPetAsync(name, petType, breed, description);

                    name = string.Empty;
                    breed = string.Empty;
                    description = string.Empty;
                }
            }
        }
    }
}