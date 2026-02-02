using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestsController(InterestsService interestsService) : ControllerBase {

  // GET: api/interests?skip=0&take=10
  [HttpGet]
  public async Task<IActionResult> GetInterests(
      [FromQuery] int skip = 0,
      [FromQuery] int take = 10
  ) {
    var (trips, total) = await interestsService.GetInterestsPage(skip, take);
    var dtoCompanions = trips
        .Select(InterestsDto.FromModel)
        .ToList();

    return Ok(new {
      items = dtoCompanions,
      total,
      skip,
      take
    });
  }

  // POST: api/interests
  [HttpPost]
  public async Task<IActionResult> CreateInterest([FromBody] InterestsDto interestsDto) {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    try {
      Interest? createdInterest = new Interest {
        Name = interestsDto.Name
      };
      createdInterest = await interestsService.CreateInterest(createdInterest);

      return Ok(InterestsDto.FromModel(createdInterest));
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }

  // DELETE: api/interests/id
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteInterest(int id) {
    try {
      await interestsService.DeleteInterest(id);
      return NoContent();
    }
    catch (Exception ex) {
      return BadRequest(ex.Message);
    }
  }
}