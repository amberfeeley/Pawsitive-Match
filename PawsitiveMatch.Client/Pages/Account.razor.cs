using MudBlazor;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Account
    {
        private async Task RemovePet(int petId)
        {
            if (State.CurrentUser != null)
            {
                await Api.RemovePetFromCartAsync(petId);
                State.CurrentUser.CartPets.RemoveAll(p => p.Id == petId);

                StateHasChanged();
            }

        }
    }
}