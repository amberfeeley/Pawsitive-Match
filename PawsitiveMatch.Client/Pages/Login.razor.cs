using MudBlazor;

namespace PawsitiveMatch.Client.Pages
{
    public partial class Login
    {
        private string EmailValue = string.Empty;
        private string PasswordValue = string.Empty;
        private string ErrorMessage = string.Empty;
        private bool success;
        private MudForm? form;

        private async Task LoginSubmission()
        {
            ErrorMessage = string.Empty;

            if (form != null)
            {
                await form.Validate();

                if (form.IsValid)
                {
                    var user = await Api.LoginAsync(EmailValue, PasswordValue);
                    if (user != null)
                    {
                        State.SetCurrentUser(user);
                        Nav.NavigateTo("/");
                    }
                    else
                    {
                        ErrorMessage = "User not found";
                    }
                }
            }
        }
    }
};