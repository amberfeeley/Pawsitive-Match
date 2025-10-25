using Microsoft.EntityFrameworkCore;
using PawsitiveMatch.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<AdoptionForm> AdoptionForm { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(u => u.AdoptionForms).WithOne().HasForeignKey(u => u.UserId).IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
            entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Password).HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasMany(a => a.AdoptionForms).WithOne().HasForeignKey(a => a.AdminId).IsRequired();
            entity.HasIndex(a => a.Email).IsUnique();
            entity.Property(a => a.Email).HasMaxLength(100).IsRequired();
            entity.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(a => a.LastName).HasMaxLength(50).IsRequired();
            entity.Property(a => a.Password).HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasMany(p => p.AdoptionForms).WithOne().HasForeignKey(p => p.PetId).IsRequired();
            entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Species).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Breed).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<AdoptionForm>(entity =>
        {
            entity.Property(f => f.Status).HasMaxLength(50).IsRequired();
            entity.Property(f => f.PhoneNumber).HasMaxLength(10).IsRequired();
        });
    }
}