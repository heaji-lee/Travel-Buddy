using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("travel_styles")]
public class TravelStyle {
  [Key]
  [Column("id")]
  public int Id { get; set; }

  [Column("name")]
  public required string Name { get; set; }
}
