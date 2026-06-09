namespace TravelBuddy.Repository.Models.DTOs;

public class TripDayDto {
  public int DayNumber { get; set; }
  public string? Notes { get; set; }

  public static TripDayDto FromModel(TripItinerary tripItinerary) {
    return new TripDayDto {
      DayNumber = tripItinerary.DayNumber,
      Notes = tripItinerary.Notes
    };
  }
}
