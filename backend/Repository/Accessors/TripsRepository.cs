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

    public async Task<(List<Trip> Items, int Total)> GetTripsPage(int skip, int take, string sortField, SortDirection sortDirection) {
        var query = _context.Trips
            .AsNoTracking()
            .AsSplitQuery();

        query = sortField?.ToLower() switch {
            "name" => sortDirection == SortDirection.Ascending
              ? query.OrderBy(t => t.Name)
              : query.OrderByDescending(t => t.Name),
            "city" => sortDirection == SortDirection.Ascending
              ? query.OrderBy(t => t.City)
              : query.OrderByDescending(t => t.City),
            "country" => sortDirection == SortDirection.Ascending
              ? query.OrderBy(t => t.Country)
              : query.OrderByDescending(t => t.Country),
            "startAt" => sortDirection == SortDirection.Ascending
              ? query.OrderBy(t => t.StartAt)
              : query.OrderByDescending(t => t.StartAt),
            "endAt" => sortDirection == SortDirection.Ascending
              ? query.OrderBy(t => t.EndAt)
              : query.OrderByDescending(t => t.EndAt),
            _ => query.OrderBy(t => t.Id)
        };

        var total = await query.CountAsync();
        var items = await query.Skip(skip).Take(take).ToListAsync();

        return (items, total);
    }

    public async Task<Trip?> GetTripById(int id) {
        return await _context.Trips
            .AsNoTracking()
            .Include(t => t.TripCompanions).ThenInclude(tc => tc.Companion)
            .Include(t => t.TripInterests).ThenInclude(ti => ti.Interest)
            .Include(t => t.TripTravelStyles).ThenInclude(tts => tts.TravelStyle)
            .AsSplitQuery()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Trip> CreateTrip(ModifyTripRequestDto dto) {
        var trip = new Trip {
            Name = dto.Name,
            City = dto.City,
            Country = dto.Country,
            StartAt = dto.StartAt,
            EndAt = dto.EndAt,
            TotalBudget = dto.TotalBudget,
            TripCompanions = dto.CompanionIds.Select(cid => new TripCompanion { CompanionId = cid }).ToList(),
            TripInterests = dto.InterestIds.Select(iid => new TripInterest { InterestId = iid }).ToList(),
            TripTravelStyles = dto.TravelStyleIds.Select(tsid => new TripTravelStyle { TravelStyleId = tsid }).ToList(),
            TripItineraries = dto.TripItineraries.Select(ti => new TripItinerary {
                DayNumber = ti.DayNumber,
                Notes = ti.Notes
            }).ToList()
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();
        return trip;
    }

    public async Task<bool> UpdateTrip(int id, ModifyTripRequestDto dto) {
        var trip = await _context.Trips
            .Include(t => t.TripCompanions)
            .Include(t => t.TripInterests)
            .Include(t => t.TripTravelStyles)
            .Include(t => t.TripItineraries)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (trip == null) return false;

        trip.Name = dto.Name;
        trip.City = dto.City;
        trip.Country = dto.Country;
        trip.StartAt = dto.StartAt;
        trip.EndAt = dto.EndAt;
        trip.TotalBudget = dto.TotalBudget;

        trip.TripCompanions.Clear();
        trip.TripInterests.Clear();
        trip.TripTravelStyles.Clear();
        trip.TripItineraries.Clear();

        trip.TripCompanions = dto.CompanionIds.Select(cid => new TripCompanion { TripId = id, CompanionId = cid }).ToList();
        trip.TripInterests = dto.InterestIds.Select(iid => new TripInterest { TripId = id, InterestId = iid }).ToList();
        trip.TripTravelStyles = dto.TravelStyleIds.Select(tsid => new TripTravelStyle { TripId = id, TravelStyleId = tsid }).ToList();
        trip.TripItineraries = dto.TripItineraries.Select(ti => new TripItinerary {
            TripId = id,
            DayNumber = ti.DayNumber,
            Notes = ti.Notes
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteTrip(int id) {
        var trip = await _context.Trips.FindAsync(id);
        if (trip == null) throw new InvalidOperationException("Trip not found");

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TripItinerary>> GetItinerary(int id) {
        return await _context.TripItineraries
          .AsNoTracking()
          .Where(ti => ti.TripId == id)
          .OrderBy(ti => ti.DayNumber)
          .ToListAsync();
    }
}
