using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Repositories;

public class DestinationsRepository {
  private readonly AppDbContext _context;
  public DestinationsRepository(AppDbContext context) {
    _context = context;
  }

  public async Task<List<Destination>> GetDestinations(string searchString) {
    if (string.IsNullOrWhiteSpace(searchString)) return new List<Destination>();

    searchString = searchString.ToLower();

    var query = _context.Destinations
      .AsNoTracking()
      .Where(d => d.City.ToLower().Contains(searchString) || d.Country.ToLower().Contains(searchString))
      .OrderBy(d => d.City)
      .Take(10);

    return await query.ToListAsync();
  }
}
