using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.SharedModels.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Password).HasMaxLength(255).IsRequired();
            entity.Property(r => r.Role).HasMaxLength(20).IsRequired();
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(u => u.Name).IsUnique();
            entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Breed).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Type).HasConversion<string>().IsRequired();
            entity.HasOne<User>()
              .WithMany(u => u.AdoptedPets)
              .HasForeignKey(p => p.OwnerId)
              .OnDelete(DeleteBehavior.SetNull);
        });
    }
}