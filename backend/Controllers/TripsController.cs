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
  public async Task<IActionResult> CreateTrip([FromBody] ModifyTripRequestDto modifyTripRequestDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    try {
      var createdTrip = await tripsService.CreateTrip(modifyTripRequestDto);
      return Ok(TripDto.FromModel(createdTrip));
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // DELETE: api/trips/{id}
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTrip(int id) {
    try {
      await tripsService.DeleteTrip(id);
      return NoContent();
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // GET: api/trips/{id}
  [HttpGet("{id}")]
  public async Task<IActionResult> GetTripById(int id) {
    try {
      var trip = await tripsService.GetTripById(id);
      var dtoTrip = trip != null ? TripDto.FromModel(trip) : null;
      return Ok(dtoTrip);
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // PUT: api/trips/{id}
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateTrip([FromRoute]int id, [FromBody] ModifyTripRequestDto modifyTripRequestDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    try {
      var existingTrip = await tripsService.GetTripById(id);
      if (existingTrip == null) {
        return NotFound($"Trip with ID {id} not found.");
      }

      var updated = await tripsService.UpdateTrip(id, modifyTripRequestDto);
      if (!updated) return NotFound($"Trip with ID {id} could not be updated");

      var updatedTrip = await tripsService.GetTripById(id);
      return Ok(TripDto.FromModel(updatedTrip));
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }
}