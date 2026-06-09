using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("trip_itineraries")]
public class TripItinerary {
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [Column("trip_id")]
  public int TripId { get; set; }
  public Trip Trip { get; set; } = null!;

  [Column("day_number")]
  public int DayNumber { get; set; }

  [Column("notes")]
  public string? Notes { get; set; }
}
