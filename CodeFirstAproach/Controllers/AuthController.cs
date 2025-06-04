using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Services;
using CodeFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = CodeFirstAproach.contracts.requests.RegisterRequest;

namespace CodeFirstAproach.Controllers;

[ApiController]
[Route("apiA/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _authService.RegisterAsync(request.Username, request.Password, cancellationToken);

        if (!success)
            return Conflict("Username is already taken.");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tokens = await _authService.LoginAsync(request.Username, request.Password, cancellationToken);

        if (tokens == null)
            return Unauthorized("Invalid username or password.");

        return Ok(tokens);
    }
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tokens = await _authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (tokens == null)
            return Unauthorized("Invalid or expired refresh token.");

        return Ok(tokens);
    }
}