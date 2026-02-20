using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Repository.Models;
using System.Text.Json.Serialization;

namespace TravelBuddy.Data;

public static class DbSeeder {
    public static async Task SeedAllAsync(AppDbContext context) {
        await SeedDestinationsAsync(context);
        await SeedCompanionsAsync(context);
        await SeedInterestsAsync(context);
        await SeedTravelStylesAsync(context);
    }

    private static async Task SeedDestinationsAsync(AppDbContext context) {
        if (await context.Destinations.AnyAsync()) return;

        var countriesPath = Path.Combine(AppContext.BaseDirectory, "Data", "countries.json");
        var citiesPath = Path.Combine(AppContext.BaseDirectory, "Data", "cities.json");

        if (!File.Exists(countriesPath) || !File.Exists(citiesPath))
            return;

        var countriesJson = await File.ReadAllTextAsync(countriesPath);
        var countriesDict = JsonSerializer.Deserialize<Dictionary<string, string>>(countriesJson)!;

        var citiesJson = await File.ReadAllTextAsync(citiesPath);
        var citiesRaw = JsonSerializer.Deserialize<List<CityRaw>>(citiesJson)!;

        var destinations = citiesRaw
          .Where(c => !string.IsNullOrWhiteSpace(c.Name) && !string.IsNullOrWhiteSpace(c.Country))
          .Select(c => new Destination {
              City = c.Name.Trim(),
              Country = countriesDict.ContainsKey(c.Country)
                  ? countriesDict[c.Country].Trim()
                  : c.Country.Trim()
          })
          .GroupBy(d => new { d.City, d.Country })
          .Select(g => g.First())
          .ToList();

        context.Destinations.AddRange(destinations);
        await context.SaveChangesAsync();
    }

    private class CityRaw {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("country")]
        public string Country { get; set; } = null!;
    }

    private static async Task SeedCompanionsAsync(AppDbContext context) {
        if (await context.Companions.AnyAsync()) return;

        var companions = new List<Companion> {
            new() { Name = "Friends" },
            new() { Name = "Helen" },
            new() { Name = "Colleagues" }
        };

        context.Companions.AddRange(companions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedInterestsAsync(AppDbContext context) {
        if (await context.Interests.AnyAsync()) return;

        var interests = new List<Interest> {
            new() { Name = "Food" },
            new() { Name = "Shopping" },
            new() { Name = "Sightseeing" }
        };

        context.Interests.AddRange(interests);
        await context.SaveChangesAsync();
    }

    private static async Task SeedTravelStylesAsync(AppDbContext context) {
        if (await context.TravelStyles.AnyAsync()) return;

        var travelStyles = new List<TravelStyle> {
            new() { Name = "Relaxing" },
            new() { Name = "Chaotic" },
            new() { Name = "Go with the flow" }
        };

        context.TravelStyles.AddRange(travelStyles);
        await context.SaveChangesAsync();
    }
}
