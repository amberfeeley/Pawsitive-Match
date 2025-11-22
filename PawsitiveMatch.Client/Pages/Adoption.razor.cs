namespace PawsitiveMatch.Client.Pages
{
    // Partial class definition needed for the Login page to detect the code
    public partial class Adoption
    {
        // Test varables
        private string EmailValue { get; set; } = string.Empty;
        private string PasswordValue { get; set; } = string.Empty;

        // Sample code for login submission. Just clears the strings for now.
        private void LoginSubmission()
        {
            if (EmailValue != string.Empty && PasswordValue != string.Empty)
            {
                EmailValue = string.Empty;
                PasswordValue = string.Empty;
            }
        }
    }
};