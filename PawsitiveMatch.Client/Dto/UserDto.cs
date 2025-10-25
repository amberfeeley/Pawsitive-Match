namespace PawsitiveMatch.Client.Dto
{
    public class UserDto(string email, string firstName, string lastName)
    {
        public string Email { get; set; } = email;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
    }
}