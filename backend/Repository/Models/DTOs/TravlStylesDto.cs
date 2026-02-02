namespace TravelBuddy.Repository.Models.DTOs;

public class TravelStyleDto {
  public int Id { get; set; }
  public required string Style { get; set; }

  public static TravelStyleDto FromModel(TravelStyle travelStyle) {
    return new TravelStyleDto {
      Id = travelStyle.Id,
      Style = travelStyle.Style
    };
  }
}
