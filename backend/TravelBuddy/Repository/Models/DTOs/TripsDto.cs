namespace TravelBuddy.Repository.Models.DTOs;

public class TripDto {
  public int Id { get; set; }
  public string City { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public DateTime StartAt { get; set; }
  public DateTime EndAt { get; set; }

  public static TripDto FromModel(Trip trip) {
    return new TripDto {
      Id = trip.Id,
      City = trip.City,
      Country = trip.Country,
      StartAt = trip.StartAt,
      EndAt = trip.EndAt
    };
  }
}
