using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class DestinationsService(DestinationsRepository destinationsRepository) {
    public async Task<List<Destination>> GetDestinations(string searchString) {
        return await destinationsRepository.GetDestinations(searchString);
    }
}
