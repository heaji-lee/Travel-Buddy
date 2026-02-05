using Microsoft.EntityFrameworkCore;
using TravelBuddy.Repository.Models;

namespace TravelBuddy.Data {
  public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Companion> Companions { get; set; }
    public DbSet<TripCompanion> TripCompanions { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<TripInterest> TripInterests { get; set; }
    public DbSet<TravelStyle> TravelStyles { get; set; }
    public DbSet<TripTravelStyle> TripTravelStyles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Trip>().ToTable("trips");
      modelBuilder.Entity<Companion>().ToTable("companions");
      modelBuilder.Entity<Interest>().ToTable("interests");
      modelBuilder.Entity<TravelStyle>().ToTable("travel_styles");
      modelBuilder.Entity<TripCompanion>().ToTable("trip_companions");
      modelBuilder.Entity<TripInterest>().ToTable("trip_interests");
      modelBuilder.Entity<TripTravelStyle>().ToTable("trip_travel_styles");

      modelBuilder.Entity<TripCompanion>()
          .HasKey(tc => new { tc.TripId, tc.CompanionId });

      modelBuilder.Entity<TripCompanion>()
          .HasOne(tc => tc.Trip)
          .WithMany(t => t.TripCompanions)
          .HasForeignKey(tc => tc.TripId);

      modelBuilder.Entity<TripCompanion>()
          .HasOne(tc => tc.Companion)
          .WithMany(c => c.TripCompanions)
          .HasForeignKey(tc => tc.CompanionId);

      modelBuilder.Entity<TripInterest>()
          .HasKey(ti => new { ti.TripId, ti.InterestId });

      modelBuilder.Entity<TripInterest>()
          .HasOne(ti => ti.Trip)
          .WithMany(t => t.TripInterests)
          .HasForeignKey(ti => ti.TripId);

      modelBuilder.Entity<TripInterest>()
          .HasOne(ti => ti.Interest)
          .WithMany(i => i.TripInterests)
          .HasForeignKey(ti => ti.InterestId);

      modelBuilder.Entity<TripTravelStyle>()
          .HasKey(tts => new { tts.TripId, tts.TravelStyleId });

      modelBuilder.Entity<TripTravelStyle>()
          .HasOne(tts => tts.Trip)
          .WithMany(t => t.TripTravelStyles)
          .HasForeignKey(tts => tts.TripId);

      modelBuilder.Entity<TripTravelStyle>()
          .HasOne(tts => tts.TravelStyle)
          .WithMany(ts => ts.TripTravelStyles)
          .HasForeignKey(tts => tts.TravelStyleId);

      modelBuilder.Entity<Trip>().HasIndex(t => t.City);
      modelBuilder.Entity<Trip>().HasIndex(t => t.StartAt);
      modelBuilder.Entity<Trip>().HasIndex(t => t.EndAt);

      modelBuilder.Entity<TripCompanion>().HasIndex(tc => new { tc.TripId, tc.CompanionId });
      modelBuilder.Entity<TripCompanion>().HasIndex(tc => tc.CompanionId);

      modelBuilder.Entity<TripInterest>().HasIndex(ti => new { ti.TripId, ti.InterestId });
      modelBuilder.Entity<TripInterest>().HasIndex(ti => ti.InterestId);

      modelBuilder.Entity<TripTravelStyle>().HasIndex(tts => new { tts.TripId, tts.TravelStyleId });
      modelBuilder.Entity<TripTravelStyle>().HasIndex(tts => tts.TravelStyleId);
    }
  }
}