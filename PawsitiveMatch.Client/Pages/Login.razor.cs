namespace PawsitiveMatch.Client.Pages
{
    // Partial class definition needed for the Login page to detect the code
    public partial class Login
    {
        // Test variables
        private string EmailValue { get; set; } = string.Empty;
        private string PasswordValue { get; set; } = string.Empty;
        private string ErrorMessage { get; set; } = string.Empty;
        private string SuccessMessage { get; set; } = string.Empty;

        private void LoginSubmission()
        {
            // Sends error if all fields are empty
            if (EmailValue == string.Empty && PasswordValue == string.Empty)
            {
                ErrorMessage = "Both fields must be filled out to login.";
                return;
            }

            // Sends error message if email field is blank
            else if (EmailValue == string.Empty)
            {
                ErrorMessage = "The Email field cannot be blank on submission.";
                return;
            }

            // Sends error message if password field is blank
            else if (PasswordValue == string.Empty)
            {
                ErrorMessage = "The Password field cannot be blank upon submission.";
                return;
            }

            // Clears email and password field upon button press. Temporary code.
            else
            {
                ErrorMessage = string.Empty;
                SuccessMessage = "Account creation was succesful! Please login to continue.";

                EmailValue = string.Empty;
                PasswordValue = string.Empty;
            }

            /*
            Future additions:
            Add admin button to either below create account
            or footer
            */
        }
    }
};