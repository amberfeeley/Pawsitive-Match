using MudBlazor;

namespace PawsitiveMatch.Client.Pages
{
    // Partial class definition needed for the Login page to detect the code
    public partial class Login
    {
        // Test variables
        private string EmailValue = string.Empty;
        private string PasswordValue = string.Empty;
        private string ErrorMessage = string.Empty;
        private string SuccessMessage = string.Empty;
        private bool success;
        private MudForm? form;

        private async Task LoginSubmission()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            if (form != null)
            {
                await form.Validate();


                    Console.WriteLine("Calling login submission");

                if (form.IsValid)
                {
                    Console.WriteLine("Calling login Async");
                    var user = await Api.LoginAsync(EmailValue, PasswordValue);
                    if (user != null)
                    {
                        State.SetCurrentUser(user);
                        SuccessMessage = $"Successfully logged in {State.CurrentUser!.FirstName} {State.CurrentUser.LastName}";
                    }
                    else
                    {
                        ErrorMessage = $"user not found";
                    }
                }
                else
                    ErrorMessage = "Email already exists!";
            }

            /*
            Future additions:
            Add admin button to either below create account
            or footer
            */
        }
    }
};