using Microsoft.EntityFrameworkCore;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Data;

public class AppDbContext : DbContext {
  public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options) {
  }

  public DbSet<Trip> Trips => Set<Trip>();
}
