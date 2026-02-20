using TravelBuddy.Repository.Models;

public class TripCompanion {
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;

    public int CompanionId { get; set; }
    public Companion Companion { get; set; } = null!;
}
