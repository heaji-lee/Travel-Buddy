using TravelBuddy.Repositories;
using TravelBuddy.Repository.Models;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class CompanionsService(CompanionsRepository companionRepository) {
  public async Task<(List<Companion> Items, int Total)> GetCompanionsPage(int skip, int take) {
    if (skip < 0) skip = 0;
    if (take <= 0) take = 10;
    if (take > 100) take = 100;

    return await companionRepository.GetCompanionsPage(skip, take);
  }

  public async Task<Companion> CreateCompanion(Companion companion) {
    return await companionRepository.CreateCompanion(companion);
  }

  public async Task DeleteCompanion(int id) {
    await companionRepository.DeleteCompanion(id);
  }
}
