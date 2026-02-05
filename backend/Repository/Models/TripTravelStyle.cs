using TravelBuddy.Repository.Models;

public class TripTravelStyle {
  public int TripId { get; set; }
  public Trip Trip { get; set; } = null!;

  public int TravelStyleId { get; set; }
  public TravelStyle TravelStyle { get; set; } = null!;
}