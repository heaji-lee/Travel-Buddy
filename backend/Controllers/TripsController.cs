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
      [FromQuery] int take = 10,
      [FromQuery] string sortField = "Id",
      [FromQuery] SortDirection sortDirection = SortDirection.Ascending
      ) {
        var (trips, total) = await tripsService.GetTripsPage(skip, take, sortField, sortDirection);

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
            var trip = await tripsService.GetTripById(createdTrip.Id);
            if (trip == null) return NotFound($"Trip with ID {createdTrip.Id} not found.");

            return Ok(TripDto.FromModel(trip));
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
            if (trip == null) return NotFound($"Trip with ID {id} not found.");

            var dtoTrip = TripDto.FromModel(trip);
            return Ok(dtoTrip);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/trips/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrip([FromRoute] int id, [FromBody] ModifyTripRequestDto modifyTripRequestDto) {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        try {
            var existingTrip = await tripsService.GetTripById(id);
            if (existingTrip == null) {
                return NotFound($"Trip with ID {id} not found.");
            }

            var updated = await tripsService.UpdateTrip(id, modifyTripRequestDto);
            if (!updated) return NotFound($"Trip with ID {id} could not be updated");

            var updatedTrip = await tripsService.GetTripById(id);
            if (updatedTrip == null) return NotFound($"Trip with ID {id} not found.");

            return Ok(TripDto.FromModel(updatedTrip));
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/{id}/itinerary
    [HttpGet("{id}/itinerary")]
    public async Task<IActionResult> GetItinerary([FromRoute] int id) {
        var days = await tripsService.GetItinerary(id);

        var dtoTripDays = days.Select(TripDayDto.FromModel);
        return Ok(dtoTripDays);
    }
}
