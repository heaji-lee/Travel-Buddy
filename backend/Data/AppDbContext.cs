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
}
