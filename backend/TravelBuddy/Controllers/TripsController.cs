using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController(TripsService tripsService) : ControllerBase {

  // GET: api/trips?skip=0&take=10
  [HttpGet]
  public async Task<IActionResult> GetTrips(
      [FromQuery] int skip = 0,
      [FromQuery] int take = 10
  ) {
    var (trips, total) = await tripsService.GetTripsPage(skip, take);

    var dtoTrips = trips
        .Select(TripDto.FromModel)
        .ToList();

    return Ok(new {
      items = dtoTrips,
      total,
      skip,
      take
    });
  }
}
