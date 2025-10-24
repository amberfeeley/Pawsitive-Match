using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Pet> Pet { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<User> User { get; set; }
}