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
    public DbSet<Room> Rooms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Studio>().HasOne(u => u.User).WithMany(s => s.Studios).HasForeignKey(u=>u.UserId);
        modelBuilder.Entity<Studio>().HasMany(s=>s.Rooms).WithOne(s => s.Studio).HasForeignKey(r=>r.StudioId);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);  
    }
}