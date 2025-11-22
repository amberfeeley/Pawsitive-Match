using Microsoft.AspNetCore.Components;
using MudBlazor;
using PawsitiveMatch.Client.Services;

namespace PawsitiveMatch.Client.Pages
{
    public partial class CreateAccount
    {
        private string FirstName = string.Empty;
        private string LastName = string.Empty;
        private string EmailValue = string.Empty;
        private string PasswordValue = string.Empty;
        private string RepeatPasswordValue = string.Empty;
        private string ErrorMessage = string.Empty;
        private bool success;
        private MudForm? form;

        private string? PasswordMatch(string arg)
        {
            if (PasswordValue != arg)
                return "Passwords don't match";
            return null;
        }
            
        private async Task CreateUserAccount()
        {
            ErrorMessage = string.Empty;

            if (form != null && Api != null)
            {
                await form.Validate();

                if (form.IsValid && await Api.RegisterAsync(EmailValue, PasswordValue, FirstName, LastName))
                {
                    Nav.NavigateTo("/");
                }
                else
                    ErrorMessage = "Email already exists!";
            }
        }
    }
};