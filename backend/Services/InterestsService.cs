using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class InterestsService(InterestsRepository interestsRepository) {
    public async Task<(List<Interest> Items, int Total)> GetInterestsPage(int skip, int take) {
        if (skip < 0) skip = 0;
        if (take <= 0) take = 10;
        if (take > 100) take = 100;

        return await interestsRepository.GetInterestsPage(skip, take);
    }

    public async Task<Interest> CreateInterest(Interest interest) {
        return await interestsRepository.CreateInterest(interest);
    }

    public async Task DeleteInterest(int id) {
        await interestsRepository.DeleteInterest(id);
    }
}
