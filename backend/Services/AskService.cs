using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace TravelBuddy.Services;

public class OpenAiService {
  private readonly HttpClient _httpClient;
  private readonly string _apiKey;
  private readonly ILogger<OpenAiService> _logger;

  public OpenAiService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenAiService> logger) {
    _httpClient = httpClient;
    _logger = logger;
    _apiKey = Environment.GetEnvironmentVariable("OPENAI")
        ?? Environment.GetEnvironmentVariable("OpenAI__ApiKey")
        ?? Environment.GetEnvironmentVariable("OpenAI:ApiKey")
        ?? configuration["OpenAI:ApiKey"]
        ?? configuration["OPENAI"]
        ?? configuration["OpenAI__ApiKey"]
        ?? string.Empty;

    _httpClient.BaseAddress = new Uri("https://api.openai.com/");
    _httpClient.Timeout = TimeSpan.FromSeconds(60);
  }

  public async Task<List<string>> GenerateItinerary(
      string city,
      int days,
      string? preferences,
      CancellationToken cancellationToken = default) {
    if (string.IsNullOrWhiteSpace(city)) {
      throw new ArgumentException("City is required.", nameof(city));
    }

    if (days <= 0) {
      throw new ArgumentOutOfRangeException(nameof(days));
    }

    var prompt = string.Format(
        "Create a {0}-day itinerary for {1}.\n\nPreferences:\n{2}\n\nReturn JSON only with this shape:\n{{\n  \"itinerary\": [\n    \"Day 1: ...\",\n    \"Day 2: ...\"\n  ]\n}}\n",
        days,
        city,
        preferences ?? "None");

    if (string.IsNullOrWhiteSpace(_apiKey)) {
      return BuildFallbackItinerary(city, days, preferences);
    }

    var payload = new {
      model = "gpt-4.1-mini",
      messages = new[]
        {
                new { role = "system", content = "You are a helpful travel planner. Return only valid JSON." },
                new { role = "user", content = prompt }
            },
      temperature = 0.7
    };

    try {
      using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/chat/completions");
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
      request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

      using var response = await _httpClient.SendAsync(request, cancellationToken);
      var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

      if (!response.IsSuccessStatusCode) {
        _logger.LogWarning("OpenAI request failed with status {StatusCode}: {Response}", response.StatusCode, responseContent);
        return BuildFallbackItinerary(city, days, preferences);
      }

      var content = ExtractAssistantContent(responseContent);
      if (string.IsNullOrWhiteSpace(content)) {
        return BuildFallbackItinerary(city, days, preferences);
      }

      var normalizedContent = NormalizeContent(content);
      if (string.IsNullOrWhiteSpace(normalizedContent)) {
        return BuildFallbackItinerary(city, days, preferences);
      }

      var parsed = JsonSerializer.Deserialize<ItineraryResponse>(normalizedContent, new JsonSerializerOptions {
        PropertyNameCaseInsensitive = true
      });

      if (parsed?.Itinerary is { Count: > 0 }) {
        return parsed.Itinerary.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
      }
    }
    catch (Exception ex) {
      _logger.LogError(ex, "OpenAI itinerary generation failed for {City}", city);
    }

    return BuildFallbackItinerary(city, days, preferences);
  }

  private static string ExtractAssistantContent(string responseContent) {
    try {
      using var document = JsonDocument.Parse(responseContent);
      var root = document.RootElement;

      return root.GetProperty("choices")[0]
          .GetProperty("message")
          .GetProperty("content")
          .GetString() ?? string.Empty;
    }
    catch {
      var start = responseContent.IndexOf('{');
      var end = responseContent.LastIndexOf('}');

      if (start >= 0 && end > start) {
        return responseContent[start..(end + 1)];
      }

      return responseContent;
    }
  }

  private static string NormalizeContent(string content) {
    if (string.IsNullOrWhiteSpace(content)) {
      return string.Empty;
    }

    var trimmed = content.Trim();

    if (trimmed.StartsWith("```")) {
      trimmed = trimmed.Replace("```json", string.Empty, StringComparison.OrdinalIgnoreCase)
          .Replace("```", string.Empty, StringComparison.OrdinalIgnoreCase)
          .Trim();
    }

    if (trimmed.StartsWith("{")) {
      return trimmed;
    }

    var start = trimmed.IndexOf('{');
    var end = trimmed.LastIndexOf('}');

    if (start >= 0 && end > start) {
      return trimmed[start..(end + 1)];
    }

    return trimmed;
  }

  private static List<string> BuildFallbackItinerary(string city, int days, string? preferences) {
    var personalization = string.IsNullOrWhiteSpace(preferences)
        ? string.Empty
        : $" with a focus on {preferences}";

    return Enumerable.Range(1, days)
        .Select(day => $"Day {day}: Discover the highlights of {city}{personalization}")
        .ToList();
  }

  private sealed class ItineraryResponse {
    public List<string>? Itinerary { get; set; }
  }
}