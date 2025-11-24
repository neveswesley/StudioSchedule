using Microsoft.EntityFrameworkCore;
using StudioSchedule.Domain.Entities;

namespace StudioSchedule.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }   
    
    public DbSet<User> Users { get; set; }
    public DbSet<Studio> Studios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Studio>().HasOne(u => u.User).WithMany(s => s.Studios);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);  
    }
}