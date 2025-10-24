public class Pet {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int? OwnerId { get; set; }
    public bool Adopted { get; set; }
}