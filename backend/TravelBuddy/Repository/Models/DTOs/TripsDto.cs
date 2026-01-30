namespace TravelBuddy.Repository.Models.DTOs;

public class TripDto {
  public int Id { get; set; }
  public string Location { get; set; } = string.Empty;
  public string Period { get; set; } = string.Empty;

  public static TripDto FromModel(Trip trip) {
    return new TripDto {
      Id = trip.Id,
      Location = $"{trip.City}, {trip.Country}",
      Period = $"{trip.StartAt:yyyy-MM-dd} to {trip.EndAt:yyyy-MM-dd}"
    };
  }
}
