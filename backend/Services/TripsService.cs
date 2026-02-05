using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class TripsService(TripsRepository tripsRepository) {
  public async Task<(List<Trip> Items, int Total)> GetTripsPage(int skip, int take) {
    if (skip < 0) skip = 0;
    if (take <= 0) take = 10;
    if (take > 100) take = 100;

    return await tripsRepository.GetTripsPage(skip, take);
  }

  public async Task<Trip> CreateTrip(ModifyTripRequestDto modifyTripRequestDto) {
    return await tripsRepository.CreateTrip(modifyTripRequestDto);
  }

  public async Task DeleteTrip(int id) {
    await tripsRepository.DeleteTrip(id);
  }

  public async Task<Trip> GetTripById(int id) {
    return await tripsRepository.GetTripById(id);
  }

  public async Task<bool> UpdateTrip(int id, ModifyTripRequestDto modifyTripRequestDto) {
    return await tripsRepository.UpdateTrip(id, modifyTripRequestDto);
  }
}
