using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using MudBlazor;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Checkout
    {
        private MudForm? form;
        private bool submitted = false;

        private async Task CheckoutPets()
        {
            if (State.CurrentUser != null)
            {
                var pets = State.CurrentUser.AdoptedPets;

                if (form != null)
                {
                    await form.Validate();

                    if (form.IsValid)
                    {
                        // Update OwnerID of each pet to CurrentUserID
                        // Each pet should have Adopted = true
                        State.CurrentUser.AdoptedPets.Clear();
                    }
                }

                Snackbar.Add("Success! Please check your email for additional information.", Severity.Success);
                submitted = true;
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("An error has occurred. Please try to submit your form again.", Severity.Error);
            }

        }
    }
}