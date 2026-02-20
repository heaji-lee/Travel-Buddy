using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class TravelStylesService(TravelStylesRepository travelStylesRepository) {
    public async Task<(List<TravelStyle> Items, int Total)> GetTravelStylesPage(int skip, int take) {
        if (skip < 0) skip = 0;
        if (take <= 0) take = 10;
        if (take > 100) take = 100;

        return await travelStylesRepository.GetTravelStylesPage(skip, take);
    }

    public async Task<TravelStyle> CreateTravelStyle(TravelStyle travelStyle) {
        return await travelStylesRepository.CreateTravelStyle(travelStyle);
    }

    public async Task DeleteTravelStyle(int id) {
        await travelStylesRepository.DeleteTravelStyle(id);
    }
}
