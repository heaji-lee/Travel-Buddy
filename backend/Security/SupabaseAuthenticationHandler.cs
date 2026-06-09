using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TravelBuddy.Services;

namespace TravelBuddy.Security;

public class SupabaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
  private readonly AuthService _authService;

  public SupabaseAuthenticationHandler(
      IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock,
      AuthService authService) : base(options, logger, encoder, clock) {
    _authService = authService;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
    if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValues)) {
      return AuthenticateResult.NoResult();
    }

    var authHeader = authHeaderValues.FirstOrDefault();
    if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) {
      return AuthenticateResult.NoResult();
    }

    var token = authHeader.Substring("Bearer ".Length).Trim();
    if (string.IsNullOrEmpty(token)) {
      return AuthenticateResult.NoResult();
    }

    try {
      var user = await _authService.GetUserAsync(token);
      if (user == null) {
        return AuthenticateResult.Fail("Invalid token");
      }

      var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.Id ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty)
            };

      if (!string.IsNullOrEmpty(user.Role)) {
        claims.Add(new Claim(ClaimTypes.Role, user.Role));
      }

      var identity = new ClaimsIdentity(claims, Scheme.Name);
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, Scheme.Name);
      return AuthenticateResult.Success(ticket);
    }
    catch (HttpRequestException ex) {
      Logger.LogWarning(ex, "Supabase token validation failed");
      return AuthenticateResult.Fail("Invalid token");
    }
  }
}
