using TravelBuddy.Repository.Models;

public class TripDay {
  public int TripId { get; set; }

  public int DayNumber { get; set; }
  public string? Notes { get; set; } = null!;
}