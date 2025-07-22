using Microsoft.EntityFrameworkCore;
using RentManagementAPI.Models; 
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } // שם ה-DbSet: Users (לשם הקלאס User)
    public DbSet<Tenant> Tenants { get; set; } // שם ה-DbSet: Tenants (לשם הקלאס Tenant)
    public DbSet<Payment> Payments { get; set; } // שם ה-DbSet: Payments (לשם הקלאס Payment)

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}