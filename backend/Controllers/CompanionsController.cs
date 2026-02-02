using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanionsController(CompanionsService companionsService) : ControllerBase {

  // GET: api/companions?skip=0&take=10
  [HttpGet]
  public async Task<IActionResult> GetCompanions(
      [FromQuery] int skip = 0,
      [FromQuery] int take = 10
  ) {
    var (trips, total) = await companionsService.GetCompanionsPage(skip, take);
    var dtoCompanions = trips
        .Select(CompanionDto.FromModel)
        .ToList();

    return Ok(new {
      items = dtoCompanions,
      total,
      skip,
      take
    });
  }

  // POST: api/companions/
  [HttpPost]
  public async Task<IActionResult> CreateCompanion([FromBody] CompanionDto companionDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    try {
      Companion? createdCompanion = new Companion {
        Name = companionDto.Name
      };
      createdCompanion = await companionsService.CreateCompanion(createdCompanion);

      return Ok(CompanionDto.FromModel(createdCompanion));
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // DELETE: api/companions/id
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCompanion(int id) {
    try {
      await companionsService.DeleteCompanion(id);
      return NoContent();
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }
}