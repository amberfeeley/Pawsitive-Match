namespace PawsitiveMatch.Client.Dto
{
    public class RegisterUserDto(string email, string password, string firstName, string lastName)
    {

        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
    }
}