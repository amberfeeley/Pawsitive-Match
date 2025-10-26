using System.ComponentModel.DataAnnotations;

namespace PawsitiveMatch.SharedModels
{
    public class AdoptionForm
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int UserId { get; set; }
        public int AdminId { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        public bool Approved { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}