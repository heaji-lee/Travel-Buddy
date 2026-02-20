using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("interests")]
public class Interest {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }
    public ICollection<TripInterest> TripInterests { get; set; } = new List<TripInterest>();
}
