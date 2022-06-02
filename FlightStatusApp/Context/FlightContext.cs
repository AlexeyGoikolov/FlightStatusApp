using FlightStatusApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightStatusApp.Context;

public class FlightContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Flight> Statuses { get; set; }

    public FlightContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(RoleConfigure);
        modelBuilder.Entity<User>(UserConfigure);
        modelBuilder.Entity<Flight>(FlightConfigure);
        
    }
    
    public void RoleConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles").HasKey(r => r.Id);
        builder.Property(r => r.Code).IsRequired().HasMaxLength(256);
        builder.HasIndex(r => r.Code).IsUnique();
        
    }
    public void UserConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(256);
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Password).IsRequired().HasMaxLength(256);
        
    }
    
    public void FlightConfigure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("Statuses").HasKey(s => s.Id);
        builder.Property(s => s.Origin).IsRequired().HasMaxLength(256);
        builder.Property(s => s.Destination).IsRequired().HasMaxLength(256);

    }
}