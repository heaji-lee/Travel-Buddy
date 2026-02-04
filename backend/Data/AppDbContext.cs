using Microsoft.EntityFrameworkCore;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Data;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options) {
  }

  public DbSet<Trip> Trips => Set<Trip>();
  public DbSet<Companion> Companions => Set<Companion>();
  public DbSet<Interest> Interests => Set<Interest>();
  public DbSet<TravelStyle> TravelStyles => Set<TravelStyle>();

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Interest>().ToTable("interests");
    modelBuilder.Entity<Companion>().ToTable("companions");
    modelBuilder.Entity<TravelStyle>().ToTable("travel_styles");

    modelBuilder.Entity<Trip>().ToTable("trips");

    modelBuilder.Entity<Trip>()
        .HasMany(t => t.Companions)
        .WithMany()
        .UsingEntity(j => j.ToTable("trip_companions"));

    modelBuilder.Entity<Trip>()
        .HasMany(t => t.Interests)
        .WithMany()
        .UsingEntity(j => j.ToTable("trip_interests"));

    modelBuilder.Entity<Trip>()
        .HasMany(t => t.TravelStyles)
        .WithMany()
        .UsingEntity(j => j.ToTable("trip_travel_styles"));
  }
}
