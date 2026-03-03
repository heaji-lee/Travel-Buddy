using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Repository.Models;

[Table("trip_budget")]
public class TripBudget {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("trip_id")]
    public int TripId { get; set; }

    [Column("category")]
    public BudgetCategoryType Category { get; set; }

    [Column("allocated_budget")]
    public int AllocatedAmount { get; set; }

    [ForeignKey(nameof(TripId))]
    public Trip Trip { get; set; } = null!;
}
