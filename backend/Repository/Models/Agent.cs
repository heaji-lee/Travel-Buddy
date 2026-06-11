public class GenerateTripRequest {
  public string City { get; set; } = string.Empty;
  public int Days { get; set; } 
  public string? Preferences { get; set; }
}

public class GenerateTripResponse {
  public List<string> Itinerary { get; set; } = [];
}