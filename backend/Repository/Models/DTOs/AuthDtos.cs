using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravelBuddy.Repository.Models.DTOs;

public class LoginRequestDto {
  public required string Email { get; set; }
  public required string Password { get; set; }
}

public class RefreshTokenRequestDto {
  public required string RefreshToken { get; set; }
}

public class AuthResponseDto {
  [JsonPropertyName("access_token")]
  public string? AccessToken { get; set; }

  [JsonPropertyName("expires_in")]
  public int? ExpiresIn { get; set; }

  [JsonPropertyName("refresh_token")]
  public string? RefreshToken { get; set; }

  [JsonPropertyName("token_type")]
  public string? TokenType { get; set; }

  [JsonPropertyName("provider_token")]
  public string? ProviderToken { get; set; }

  [JsonPropertyName("provider_refresh_token")]
  public string? ProviderRefreshToken { get; set; }

  [JsonPropertyName("user")]
  public SupabaseUserDto? User { get; set; }

  [JsonPropertyName("error")]
  public string? Error { get; set; }

  [JsonPropertyName("error_description")]
  public string? ErrorDescription { get; set; }
}

public class SupabaseUserDto {
  [JsonPropertyName("id")]
  public string? Id { get; set; }

  [JsonPropertyName("email")]
  public string? Email { get; set; }

  [JsonPropertyName("email_confirmed_at")]
  public DateTime? EmailConfirmedAt { get; set; }

  [JsonPropertyName("phone_confirmed_at")]
  public DateTime? PhoneConfirmedAt { get; set; }

  [JsonPropertyName("confirmed_at")]
  public DateTime? ConfirmedAt { get; set; }

  [JsonPropertyName("created_at")]
  public DateTime? CreatedAt { get; set; }

  [JsonPropertyName("updated_at")]
  public DateTime? UpdatedAt { get; set; }

  [JsonPropertyName("role")]
  public string? Role { get; set; }

  [JsonPropertyName("app_metadata")]
  public JsonElement? AppMetaData { get; set; }

  [JsonPropertyName("user_metadata")]
  public JsonElement? UserMetaData { get; set; }

  [JsonIgnore]
  public string? FullName => GetMetadataString("full_name", "fullName", "name");

  [JsonIgnore]
  public string? DisplayName => GetMetadataString("display_name", "displayName", "full_name", "fullName", "name");

  private string? GetMetadataString(params string[] keys) {
    if (UserMetaData is not JsonElement metadata) {
      return null;
    }

    foreach (var key in keys) {
      if (metadata.TryGetProperty(key, out var element) && element.ValueKind == JsonValueKind.String) {
        return element.GetString();
      }
    }

    return null;
  }
}

public class SignUpRequestDto {
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string FullName { get; set; }
}
