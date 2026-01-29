using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sbase = Supabase.Postgrest.Attributes;

namespace TravelBuddy.Models;

[Table("Trips")]
[Sbase.Table("Trips")]
public class Trip {
  [Key]
  [Sbase.PrimaryKey("id", false)]
  [Column("id")]
  [Sbase.Column("id")]
  public int Id { get; set; }

  [Column("city")]
  [Sbase.Column("city")]
  public string City { get; set; } = string.Empty;

  [Column("country")]
  [Sbase.Column("country")]
  public string Country { get; set; } = string.Empty;

  [Column("start_at")]
  [Sbase.Column("start_at")]
  public DateTime StartAt { get; set; }

  [Column("end_at")]
  [Sbase.Column("end_at")]
  public DateTime EndAt { get; set; }
}