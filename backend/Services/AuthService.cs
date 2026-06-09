using System.Net.Http.Headers;
using System.Net.Http.Json;
using TravelBuddy.Repository.Models.DTOs;

namespace TravelBuddy.Services;

public class AuthService {
  private const string SupabaseAuthPath = "auth/v1";
  private readonly HttpClient _httpClient;

  public AuthService(HttpClient httpClient) {
    _httpClient = httpClient;
  }

  public async Task<AuthResponseDto> SignInAsync(string email, string password, CancellationToken cancellationToken = default) {
    var request = new { email, password };
    using var response = await _httpClient.PostAsJsonAsync($"{SupabaseAuthPath}/token?grant_type=password", request, cancellationToken);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: cancellationToken)!
        ?? throw new InvalidOperationException("Failed to deserialize auth response.");
  }

  public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default) {
    var request = new { refresh_token = refreshToken };
    using var response = await _httpClient.PostAsJsonAsync($"{SupabaseAuthPath}/token?grant_type=refresh_token", request, cancellationToken);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: cancellationToken)!
        ?? throw new InvalidOperationException("Failed to deserialize auth response.");
  }

  public async Task<SupabaseUserDto> GetUserAsync(string accessToken, CancellationToken cancellationToken = default) {
    using var request = new HttpRequestMessage(HttpMethod.Get, $"{SupabaseAuthPath}/user");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    using var response = await _httpClient.SendAsync(request, cancellationToken);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<SupabaseUserDto>(cancellationToken: cancellationToken)!
        ?? throw new InvalidOperationException("Failed to deserialize user response.");
  }

  public async Task<AuthResponseDto> SignUpAsync(string email, string password, string fullName, CancellationToken cancellationToken = default) {
    var request = new {
      email,
      password,
      data = new {
        full_name = fullName,
        display_name = fullName
      }
    };

    using var response = await _httpClient.PostAsJsonAsync(
      $"{SupabaseAuthPath}/signup",
      request,
      cancellationToken
    );

    var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
    if (!response.IsSuccessStatusCode) {
      throw new HttpRequestException(
        $"Supabase signup failed ({(int)response.StatusCode}): {responseBody}",
        null,
        response.StatusCode);
    }

    return await response.Content.ReadFromJsonAsync<AuthResponseDto>(cancellationToken: cancellationToken)
      ?? throw new InvalidOperationException("Failed to deserialise signup response.");
  }
}
