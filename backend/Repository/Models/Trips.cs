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

  [Column("start_at")]
  public DateTime StartAt { get; set; }

  [Column("end_at")]
  public DateTime EndAt { get; set; }

  public ICollection<Companion> Companions { get; set; } = new List<Companion>();
  public ICollection<Interest> Interests { get; set; } = new List<Interest>();
  public ICollection<TravelStyle> TravelStyles { get; set; } = new List<TravelStyle>();
}
