using Microsoft.EntityFrameworkCore;
using TravelBuddy.Models; // Ensure this matches your folder structure

namespace TravelBuddy.Data;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
  }

  public DbSet<Trip> Trips { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.HasDefaultSchema("public");

    // This explicitly tells EF that our Trip class maps to the Trips table
    modelBuilder.Entity<Trip>().ToTable("Trips");

    base.OnModelCreating(modelBuilder);
  }
}