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

    public async Task<(List<Trip> Items, int Total)> GetTripsPage(int skip, int take) {
        var query = _context.Trips
            .AsNoTracking()
            .Include(t => t.TripCompanions).ThenInclude(tc => tc.Companion)
            .Include(t => t.TripInterests).ThenInclude(ti => ti.Interest)
            .Include(t => t.TripTravelStyles).ThenInclude(tts => tts.TravelStyle)
            .AsSplitQuery()
            .OrderBy(t => t.Id);

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
            StartAt = dto.StartAt,
            EndAt = dto.EndAt,
            TripCompanions = dto.CompanionIds.Select(cid => new TripCompanion { CompanionId = cid }).ToList(),
            TripInterests = dto.InterestIds.Select(iid => new TripInterest { InterestId = iid }).ToList(),
            TripTravelStyles = dto.TravelStyleIds.Select(tsid => new TripTravelStyle { TravelStyleId = tsid }).ToList()
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
            .FirstOrDefaultAsync(t => t.Id == id);

        if (trip == null) return false;

        trip.Name = dto.Name;
        trip.City = dto.City;
        trip.StartAt = dto.StartAt;
        trip.EndAt = dto.EndAt;

        // Clear existing relationships
        trip.TripCompanions.Clear();
        trip.TripInterests.Clear();
        trip.TripTravelStyles.Clear();

        // Add new relationships
        trip.TripCompanions = dto.CompanionIds.Select(cid => new TripCompanion { TripId = id, CompanionId = cid }).ToList();
        trip.TripInterests = dto.InterestIds.Select(iid => new TripInterest { TripId = id, InterestId = iid }).ToList();
        trip.TripTravelStyles = dto.TravelStyleIds.Select(tsid => new TripTravelStyle { TripId = id, TravelStyleId = tsid }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteTrip(int id) {
        var trip = await _context.Trips.FindAsync(id);
        if (trip == null) throw new InvalidOperationException("Trip not found");

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
    }
}