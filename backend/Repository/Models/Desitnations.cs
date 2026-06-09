using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("cities")]
public class Destination {
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [Column("city")]
  public required string City { get; set; }

  [Column("country")]
  public required string Country { get; set; }
}
