namespace TravelBuddy.Repository.Models.DTOs;

public class LoginRequestDto {
  public required string Email { get; set; }
  public required string Password { get; set; }
}

public class RefreshTokenRequestDto {
  public required string RefreshToken { get; set; }
}

public class AuthResponseDto {
  public string? AccessToken { get; set; }
  public string? ExpiresIn { get; set; }
  public string? RefreshToken { get; set; }
  public string? TokenType { get; set; }
  public string? ProviderToken { get; set; }
  public string? ProviderRefreshToken { get; set; }
  public string? User { get; set; }
  public string? Error { get; set; }
  public string? ErrorDescription { get; set; }
}

public class SupabaseUserDto {
  public string? Id { get; set; }
  public string? Email { get; set; }
  public bool EmailConfirmed { get; set; }
  public bool PhoneConfirmed { get; set; }
  public bool ConfirmedAt { get; set; }
  public DateTime? CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public string? Role { get; set; }
  public object? AppMetaData { get; set; }
  public object? UserMetaData { get; set; }
}

public class SignUpRequestDto {
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string FullName { get; set; }
}
