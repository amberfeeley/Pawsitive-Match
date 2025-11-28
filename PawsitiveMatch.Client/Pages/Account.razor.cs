using MudBlazor;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Account
    {
        private void RemovePet(int petId)
        {
            if (State.CurrentUser != null)
            {
                State.CurrentUser.AdoptedPets.RemoveAll(p => p.Id == petId);
                StateHasChanged();
            }

        }
    }
}