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
        public DbSet<TripItinerary> TripItineraries { get; set; }
        public DbSet<Destination> Destinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>().ToTable("trips");
            modelBuilder.Entity<Companion>().ToTable("companions");
            modelBuilder.Entity<Interest>().ToTable("interests");
            modelBuilder.Entity<TravelStyle>().ToTable("travel_styles");
            modelBuilder.Entity<TripCompanion>().ToTable("trip_companions");
            modelBuilder.Entity<TripInterest>().ToTable("trip_interests");
            modelBuilder.Entity<TripTravelStyle>().ToTable("trip_travel_styles");
            modelBuilder.Entity<TripItinerary>().ToTable("trip_itineraries");
            modelBuilder.Entity<Destination>().ToTable("destinations");

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

            modelBuilder.Entity<TripItinerary>()
                .HasOne(ti => ti.Trip)
                .WithMany(t => t.TripItineraries)
                .HasForeignKey(ti => ti.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trip>().HasIndex(t => t.City);
            modelBuilder.Entity<Trip>().HasIndex(t => t.StartAt);
            modelBuilder.Entity<Trip>().HasIndex(t => t.EndAt);

            modelBuilder.Entity<TripCompanion>().HasIndex(tc => new { tc.TripId, tc.CompanionId });
            modelBuilder.Entity<TripCompanion>().HasIndex(tc => tc.CompanionId);

            modelBuilder.Entity<TripInterest>().HasIndex(ti => new { ti.TripId, ti.InterestId });
            modelBuilder.Entity<TripInterest>().HasIndex(ti => ti.InterestId);

            modelBuilder.Entity<TripTravelStyle>().HasIndex(tts => new { tts.TripId, tts.TravelStyleId });
            modelBuilder.Entity<TripTravelStyle>().HasIndex(tts => tts.TravelStyleId);

            modelBuilder.Entity<TripItinerary>().HasIndex(ti => new { ti.TripId, ti.DayNumber }).IsUnique();
            modelBuilder.Entity<TripItinerary>().HasIndex(ti => ti.TripId);

            modelBuilder.Entity<Destination>().HasIndex(d => d.City);
            modelBuilder.Entity<Destination>().HasIndex(d => d.Country);
            modelBuilder.Entity<Destination>().HasIndex(d => new { d.City, d.Country }).IsUnique();
        }
    }
}
