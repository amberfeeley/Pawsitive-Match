using System.ComponentModel.DataAnnotations;

namespace PawsitiveMatch.SharedModels.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public PetType Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Breed { get; set; } = string.Empty;
        public int? OwnerId { get; set; }
        public bool Adopted { get; set; } = false;
        public List<AdoptionForm> AdoptionForms { get; set; } = [];
        public string Image { get; set; }
    }
}