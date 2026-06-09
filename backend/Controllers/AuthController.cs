using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Repository.Models.DTOs;
using TravelBuddy.Services;

namespace TravelBuddy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase {

  // POST: api/auth/login
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDto request) {
    if (!ModelState.IsValid) {
      return BadRequest(ModelState);
    }

    try {
      var result = await authService.SignInAsync(request.Email, request.Password);
      return Ok(ToClientAuthResponse(result));
    }
    catch (HttpRequestException ex) {
      return Unauthorized(new { error = "Invalid credentials", detail = ex.Message });
    }
  }

  // POST: api/auth/refresh
  [HttpPost("refresh")]
  public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request) {
    if (!ModelState.IsValid) {
      return BadRequest(ModelState);
    }

    try {
      var result = await authService.RefreshTokenAsync(request.RefreshToken);
      return Ok(ToClientAuthResponse(result));
    }
    catch (HttpRequestException ex) {
      return Unauthorized(new { error = "Invalid refresh token", detail = ex.Message });
    }
  }

  // GET: api/auth/me
  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> Me() {
    var authHeader = Request.Headers["Authorization"].FirstOrDefault();
    if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ")) {
      return Unauthorized();
    }

    var token = authHeader.Substring("Bearer ".Length).Trim();
    try {
      var user = await authService.GetUserAsync(token);
      return Ok(user);
    }
    catch (HttpRequestException) {
      return Unauthorized();
    }
  }

  private static object ToClientAuthResponse(AuthResponseDto result) {
    return new {
      accessToken = result.AccessToken,
      refreshToken = result.RefreshToken,
      fullName = result.User?.FullName ?? result.User?.DisplayName,
      email = result.User?.Email,
      id = result.User?.Id
    };
  }

  // POST: api/auth/register
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] SignUpRequestDto request) {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    try {
      var result = await authService.SignUpAsync(request.Email, request.Password, request.FullName);
      return Ok(ToClientAuthResponse(result));
    }
    catch (HttpRequestException ex) {
      return BadRequest(new {
        error = "Registration Failed",
        detail = ex.Message
      });
    }
  }
}
