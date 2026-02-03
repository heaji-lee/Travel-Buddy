using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Repositories;

public class CompanionsRepository {
  private readonly AppDbContext _context;
  public CompanionsRepository(AppDbContext context) {
    _context = context;
  }

  public async Task<(List<Companion> Items, int Total)> GetCompanionsPage(
      int skip,
      int take
  ) {
    var query = _context.Companions.AsNoTracking();
    var items = await query
        .Skip(skip)
        .Take(take)
        .ToListAsync();

    var total = items.Count;
    return (items, total);
  }

  public async Task<Companion> CreateCompanion (Companion companion) {
    _context.Companions.Add(companion);
    await _context.SaveChangesAsync();
    return companion;
  }

  public async Task DeleteCompanion(int id) {
    var companion = await _context.Companions.FindAsync(id);
    if (companion == null) {
      throw new InvalidOperationException("Trip not found");
    }

    _context.Companions.Remove(companion);
    await _context.SaveChangesAsync();
  }
}
