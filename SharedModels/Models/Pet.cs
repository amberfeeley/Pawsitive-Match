using System.ComponentModel.DataAnnotations;

namespace PawsitiveMatch.SharedModels.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public PetType Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Breed { get; set; } = string.Empty;
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
        public int? InCartOfUserId { get; set; }
        public User? InCartOfUser { get; set; }
        public bool Adopted { get; set; } = false;
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
    }
}