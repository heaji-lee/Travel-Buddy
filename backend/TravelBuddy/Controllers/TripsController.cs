using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Models;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController : ControllerBase {
  private readonly AppDbContext _context;

  public TripsController(AppDbContext context) {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult> GetTrips() {
    var trips = await _context.Trips.AsNoTracking().ToListAsync();
    return Ok(trips);
  }
}