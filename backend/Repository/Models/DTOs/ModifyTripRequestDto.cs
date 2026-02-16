namespace TravelBuddy.Repository.Models.DTOs;

public class ModifyTripRequestDto {
  public required string Name { get; set; }
  public required string City { get; set; }
  public required string Country { get; set; }
  public DateTime StartAt { get; set; }
  public DateTime EndAt { get; set; }

  public List<int> CompanionIds { get; set; } = new();
  public List<int> InterestIds { get; set; } = new();
  public List<int> TravelStyleIds { get; set; } = new();
  public List<TripDay> TripItineraries { get; set; } = new();
}