using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Repositories;

public class TripsRepository {
  private readonly AppDbContext _context;
  public TripsRepository(AppDbContext context) {
    _context = context;
  }

  public async Task<(List<Trip> Items, int Total)> GetTripsPage(
      int skip,
      int take
  ) {
    var query = _context.Trips.AsNoTracking();

    var items = await query
        .OrderBy(t => t.StartAt)
        .Skip(skip)
        .Take(take)
        .ToListAsync();

    var total = items.Count;

    return (items, total);
  }

  public async Task<Trip?> GetById(int id) {
    return await _context.Trips
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id);
  }

  public async Task<Trip> CreateTrip (Trip trip) {
    _context.Trips.Add(trip);
    await  _context.SaveChangesAsync();
    return trip;
  }
}
