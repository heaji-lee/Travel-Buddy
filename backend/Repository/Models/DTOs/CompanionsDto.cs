namespace TravelBuddy.Repository.Models.DTOs;

public class CompanionDto {
    public int Id { get; set; }
    public required string Name { get; set; }

    public static CompanionDto FromModel(Companion companion) {
        return new CompanionDto {
            Id = companion.Id,
            Name = companion.Name
        };
    }
}
