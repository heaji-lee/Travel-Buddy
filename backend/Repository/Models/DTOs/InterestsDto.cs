namespace TravelBuddy.Repository.Models.DTOs;

public class InterestDto {
  public int Id { get; set; }
  public required string Name { get; set; }

  public static InterestDto FromModel(Interest interest) {
    return new InterestDto {
      Id = interest.Id,
      Name = interest.Name
    };
  }
}
