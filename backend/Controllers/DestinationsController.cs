using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationsController(DestinationsService destinationsService) : ControllerBase {

  // GET: api/destinations?searchString=" "
  [HttpGet]
  public async Task<IActionResult> GetDestinations([FromQuery] string searchString) {
    var destinations = await destinationsService.GetDestinations(searchString);

    var dtoDestinations = destinations
        .Select(DestinationDto.FromModel)
        .ToList();

    return Ok(new { items = dtoDestinations });
  }
}