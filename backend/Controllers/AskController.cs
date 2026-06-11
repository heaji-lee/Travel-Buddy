using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Services;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AskController(OpenAiService openAiService) : ControllerBase {
  [HttpPost("generate")]
  public async Task<ActionResult<GenerateTripResponse>> Generate([FromBody] GenerateTripRequest request) {
    if (string.IsNullOrWhiteSpace(request.City)) {
      return BadRequest("City is required.");
    }

    var itinerary = await openAiService.GenerateItinerary(request.City, request.Days, request.Preferences);

    return Ok(new GenerateTripResponse {
      Itinerary = itinerary
    });
  }
}
