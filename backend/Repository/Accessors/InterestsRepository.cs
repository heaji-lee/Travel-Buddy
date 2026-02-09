using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Repositories;

public class InterestsRepository {
  private readonly AppDbContext _context;
  public InterestsRepository(AppDbContext context) {
    _context = context;
  }

  public async Task<(List<Interest> Items, int Total)> GetInterestsPage(int skip, int take) {
    var query = _context.Interests.AsNoTracking();
    var items = await query
        .OrderBy(t => t.Id)
        .Skip(skip)
        .Take(take)
        .ToListAsync();

    var total = await query.CountAsync();
    return (items, total);
  }

  public async Task<Interest> CreateInterest(Interest interest) {
    _context.Interests.Add(interest);
    await _context.SaveChangesAsync();
    return interest;
  }

  public async Task DeleteInterest(int id) {
    var interest = await _context.Interests.FindAsync(id);
    if (interest == null) {
      throw new InvalidOperationException("Trip not found");
    }

    _context.Interests.Remove(interest);
    await _context.SaveChangesAsync();
  }
}
