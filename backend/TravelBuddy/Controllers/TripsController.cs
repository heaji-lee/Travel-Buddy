using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

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

  // POST: api/trips
  [HttpPost]
  public async Task<IActionResult> CreateTrip([FromBody] TripDto tripDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    Trip? createdTrip = null;

    try {
      createdTrip = new Trip {
        City = tripDto.City,
        Country = tripDto.Country,
        StartAt = tripDto.StartAt,
        EndAt = tripDto.EndAt
      };
      createdTrip = await tripsService.CreateTrip(createdTrip);

      return Ok(TripDto.FromModel(createdTrip));
    } catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }
}
