namespace TravelBuddy.Repository.Models.DTOs;

public class InterestsDto {
  public int Id { get; set; }
  public required string Name { get; set; }

  public static InterestsDto FromModel(Interest interest) {
    return new InterestsDto {
      Id = interest.Id,
      Name = interest.Name
    };
  }
}
