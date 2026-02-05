using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Repositories;

public class TravelStylesRepository {
  private readonly AppDbContext _context;
  public TravelStylesRepository(AppDbContext context) {
    _context = context;
  }

  public async Task<(List<TravelStyle> Items, int Total)> GetTravelStylesPage(int skip, int take) {
    var query = _context.TravelStyles.AsNoTracking();
    var items = await query
        .OrderBy(t => t.Id)
        .Skip(skip)
        .Take(take)
        .ToListAsync();

    var total = items.Count;
    return (items, total);
  }

  public async Task<TravelStyle> CreateTravelStyle (TravelStyle travelStyle) {
    _context.TravelStyles.Add(travelStyle);
    await _context.SaveChangesAsync();
    return travelStyle;
  }

  public async Task DeleteTravelStyle(int id) {
    var travelstyle = await _context.TravelStyles.FindAsync(id);
    if (travelstyle == null) {
      throw new InvalidOperationException("Trip not found");
    }

    _context.TravelStyles.Remove(travelstyle);
    await _context.SaveChangesAsync();
  }
}
