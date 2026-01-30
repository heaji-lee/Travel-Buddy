using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("trips")]
public class Trip {
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [Column("city")]
  public string City { get; set; } = string.Empty;

  [Column("country")]
  public string Country { get; set; } = string.Empty;

  [Column("start_at")]
  public DateTime StartAt { get; set; }

  [Column("end_at")]
  public DateTime EndAt { get; set; }
}
