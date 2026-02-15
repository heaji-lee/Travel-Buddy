using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationsController(DestinationsService destinationsService) : ControllerBase {

  // GET: api/destinations?search=" "
  [HttpGet]
  public async Task<IActionResult> GetDestinations([FromQuery] string search) {
    var destinations = await destinationsService.GetDestinations(search);

    var dtoDestinations = destinations
        .Select(DestinationDto.FromModel)
        .ToList();

    return Ok(dtoDestinations);
  }
}