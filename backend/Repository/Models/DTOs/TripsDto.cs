namespace TravelBuddy.Repository.Models.DTOs;

public class TripDto {
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string City { get; set; }
  public DateTime StartAt { get; set; }
  public DateTime EndAt { get; set; }

  public List<CompanionDto> Companions { get; set; } = new();
  public List<InterestDto> Interests { get; set; } = new();
  public List<TravelStyleDto> TravelStyles { get; set; } = new();

  public static TripDto FromModel(Trip trip) {
    return new TripDto {
      Id = trip.Id,
      Name = trip.Name,
      City = trip.City,
      StartAt = trip.StartAt,
      EndAt = trip.EndAt,
      Companions = trip.Companions?.Select(CompanionDto.FromModel).ToList() ?? new(),
      Interests = trip.Interests?.Select(InterestDto.FromModel).ToList() ?? new(),
      TravelStyles = trip.TravelStyles?.Select(TravelStyleDto.FromModel).ToList() ?? new()
    };
  }
}
