using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("trips")]
public class Trip {
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [Column("name")]
  public required string Name { get; set; }

  [Column("city")]
  public required string City { get; set; }

  [Column("country")]
  public required string Country { get; set; }

  [Column("start_at")]
  public DateTime StartAt { get; set; }

  [Column("end_at")]
  public DateTime EndAt { get; set; }

  public ICollection<TripCompanion> TripCompanions { get; set; } = new List<TripCompanion>();
  public ICollection<TripInterest> TripInterests { get; set; } = new List<TripInterest>();
  public ICollection<TripTravelStyle> TripTravelStyles { get; set; } = new List<TripTravelStyle>();
  public ICollection<TripItinerary> TripItineraries { get; set; } = new List<TripItinerary>();
}
