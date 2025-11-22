using System.ComponentModel.DataAnnotations;

namespace PawsitiveMatch.SharedModels.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        public List<Pet> AdoptedPets { get; set; } = [];

        public List<AdoptionForm> AdoptionForms { get; set; } = [];
    }
}