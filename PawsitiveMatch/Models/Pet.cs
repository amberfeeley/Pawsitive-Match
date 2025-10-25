using System.ComponentModel.DataAnnotations;

public class Pet {
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Species { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Breed { get; set; } = string.Empty;
    public int? OwnerId { get; set; }
    public bool Adopted { get; set; }
    public List<AdoptionForm> AdoptionForms  { get; set; } = [];
}