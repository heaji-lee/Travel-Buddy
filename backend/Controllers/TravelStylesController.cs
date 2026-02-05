using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelStylesController(TravelStylesService travelStylesService) : ControllerBase {

  // GET: api/travelStyles?skip=0&take=10
  [HttpGet]
  public async Task<IActionResult> GetTravelStyles([FromQuery] int skip = 0, [FromQuery] int take = 10) {
    var (travelStyles, total) = await travelStylesService.GetTravelStylesPage(skip, take);
    var dtoTravelStyles = travelStyles
        .Select(TravelStyleDto.FromModel)
        .ToList();

    return Ok(new {
      items = dtoTravelStyles,
      total,
      skip,
      take
    });
  }

  // POST: api/travelStyles
  [HttpPost]
  public async Task<IActionResult> CreateTravelStyle([FromBody] TravelStyleDto travelStyleDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    try {
      TravelStyle? createdTravelStyle = new TravelStyle {
        Name = travelStyleDto.Name
      };
      createdTravelStyle = await travelStylesService.CreateTravelStyle(createdTravelStyle);

      return Ok(TravelStyleDto.FromModel(createdTravelStyle));
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // DELETE: api/travelStyles/id
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTravelStyle(int id) {
    try {
      await travelStylesService.DeleteTravelStyle(id);
      return NoContent();
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }
}