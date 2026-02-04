using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

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
        .OrderBy(t => t.Id)
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

  public async Task<Trip> CreateTrip (ModifyTripRequestDto modifyTripRequestDto) {
    var companions = await _context.Companions  
      .Where(c => modifyTripRequestDto.CompanionIds.Contains(c.Id))
      .ToListAsync();

    var interests = await _context.Interests  
      .Where(c => modifyTripRequestDto.InterestIds.Contains(c.Id))
      .ToListAsync();

    var travelStyles = await _context.TravelStyles  
      .Where(c => modifyTripRequestDto.TravelStyleIds.Contains(c.Id))
      .ToListAsync();

    var trip = new Trip {
      Name = modifyTripRequestDto.Name,
      City = modifyTripRequestDto.City,
      Country = modifyTripRequestDto.Country,
      StartAt = modifyTripRequestDto.StartAt, 
      EndAt = modifyTripRequestDto.EndAt, 
      Companions = companions,
      Interests = interests, 
      TravelStyles = travelStyles
    };

    _context.Trips.Add(trip);
    await _context.SaveChangesAsync();

    return trip;
  }

  public async Task DeleteTrip(int id) {
    var trip = await _context.Trips.FindAsync(id);
    if (trip == null) {
      throw new InvalidOperationException("Trip not found");
    }

    _context.Trips.Remove(trip);
    await _context.SaveChangesAsync();
  }

  public async Task<Trip> GetTripById(int id) {
    var trip = await _context.Trips
        .AsNoTracking()
        .Include(t => t.Companions)
        .Include(t => t.Interests)
        .Include(t => t.TravelStyles)
        .AsSplitQuery()
        .FirstOrDefaultAsync(t => t.Id == id);

    if (trip == null) {
      throw new InvalidOperationException("Trip not found");
    }
    return trip;
  }

  public async Task<bool> UpdateTrip(int id, ModifyTripRequestDto modifyTripRequestDto) {
    var trip = await _context.Trips
      .Include(t => t.Companions)
      .Include(t => t.Interests)
      .Include(t => t.TravelStyles)
      .FirstOrDefaultAsync(t => t.Id == id);

    if (trip == null) return false; 

    trip.Name = modifyTripRequestDto.Name;
    trip.City = modifyTripRequestDto.City;
    trip.Country = modifyTripRequestDto.Country;
    trip.StartAt = modifyTripRequestDto.StartAt;
    trip.EndAt = modifyTripRequestDto.EndAt;

    trip.Companions = await _context.Companions
      .Where(c => modifyTripRequestDto.CompanionIds.Contains(c.Id))
      .ToListAsync();
    
    trip.Interests = await _context.Interests
      .Where(c => modifyTripRequestDto.InterestIds.Contains(c.Id))
      .ToListAsync();
    
    trip.TravelStyles = await _context.TravelStyles
      .Where(c => modifyTripRequestDto.TravelStyleIds.Contains(c.Id))
      .ToListAsync();

    await _context.SaveChangesAsync();
    return true;
  }
}
