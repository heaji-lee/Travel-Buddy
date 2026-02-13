namespace TravelBuddy.Repository.Models.DTOs;

public class DestinationDto {
  public int Id { get; set; }
  public required string City { get; set; }
  public required string Country { get; set; }

  public static DestinationDto FromModel(Destination destination) {
    return new DestinationDto {
      Id = destination.Id, 
      City = destination.City,
      Country = destination.Country,
    };
  }
}
