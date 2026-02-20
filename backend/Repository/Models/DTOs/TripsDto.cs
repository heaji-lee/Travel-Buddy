namespace TravelBuddy.Repository.Models.DTOs;

public class TripDto {
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string City { get; set; }
  public required string Country { get; set; }
  public DateTime StartAt { get; set; }
  public DateTime EndAt { get; set; }
  public float TotalBudget { get; set; }

  public List<CompanionDto> Companions { get; set; } = new();
  public List<InterestDto> Interests { get; set; } = new();
  public List<TravelStyleDto> TravelStyles { get; set; } = new();

  public static TripDto FromModel(Trip trip) {
    return new TripDto {
      Id = trip.Id, 
      Name = trip.Name,
      City = trip.City,
      Country = trip.Country,
      StartAt = trip.StartAt,
      EndAt = trip.EndAt,
      TotalBudget = trip.TotalBudget,
      Companions = trip.TripCompanions?.Select(tc => CompanionDto.FromModel(tc.Companion)).ToList() ?? new(),
      Interests = trip.TripInterests?.Select(tc => InterestDto.FromModel(tc.Interest)).ToList() ?? new(),
      TravelStyles = trip.TripTravelStyles?.Select(tts => TravelStyleDto.FromModel(tts.TravelStyle)).ToList() ?? new()
    };
  }
}
