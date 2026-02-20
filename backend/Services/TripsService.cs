using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class TripsService(TripsRepository tripsRepository) {
    public async Task<(List<Trip> Items, int Total)> GetTripsPage(int skip, int take, string sortField, SortDirection sortDirection) {
        return await tripsRepository.GetTripsPage(skip, take, sortField, sortDirection);
    }

    public async Task<Trip> CreateTrip(ModifyTripRequestDto modifyTripRequestDto) {
        return await tripsRepository.CreateTrip(modifyTripRequestDto);
    }

    public async Task DeleteTrip(int id) {
        await tripsRepository.DeleteTrip(id);
    }

    public async Task<Trip?> GetTripById(int id) {
        return await tripsRepository.GetTripById(id);
    }

    public async Task<bool> UpdateTrip(int id, ModifyTripRequestDto modifyTripRequestDto) {
        return await tripsRepository.UpdateTrip(id, modifyTripRequestDto);
    }

    public async Task<List<TripItinerary>> GetItinerary(int id) {
        return await tripsRepository.GetItinerary(id);
    }
}
