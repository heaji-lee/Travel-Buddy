using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class TripsService(TripsRepository tripsRepository) {
  public async Task<(List<Trip> Items, int Total)> GetTripsPage(
      int skip,
      int take
  ) {
    if (skip < 0) skip = 0;
    if (take <= 0) take = 10;
    if (take > 100) take = 100;

    return await tripsRepository.GetTripsPage(skip, take);
  }
}
