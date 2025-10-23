namespace PawsitiveMatch.Client.Pages
{
    // Partial class definition needed for the Login page to detect the code
    public partial class CreateAccount
    {
        // Test variables
        private string EmailValue { get; set; } = string.Empty;
        private string PasswordValue { get; set; } = string.Empty;
        private string RepeatPasswordValue { get; set; } = string.Empty;
        private string ErrorMessage { get; set; } = string.Empty;
        private string SuccessMessage { get; set; } = string.Empty;


        // Sample code for account submission. Just clears the strings for now.
        private void AccountSubmission()
        {
            // Sends error if all fields are empty
            if (EmailValue == string.Empty && PasswordValue == string.Empty && RepeatPasswordValue == string.Empty)
            {
                ErrorMessage = "All fields must be filled out to login.";
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

            // Sends error message if re-password field is blank
            else if (RepeatPasswordValue == string.Empty)
            {
                ErrorMessage = "The Re-enter Password field cannot be blank upon submission.";
                return;
            }

            // Sends error message if re-password field is blank
            else if (PasswordValue != RepeatPasswordValue)
            {
                ErrorMessage = "The Password fields do not match. Please re-enter them and submit again.";
                return;
            }

            // Clears email and password field upon button press. Temporary code.
            else
            {
                ErrorMessage = string.Empty;
                SuccessMessage = "Login successful!";

                EmailValue = string.Empty;
                PasswordValue = string.Empty;
            }
        }
    }
};