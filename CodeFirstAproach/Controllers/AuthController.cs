using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAproach.Controllers;

[ApiController]
[Route("apiA/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var success = await _authService.RegisterAsync(request, cancellationToken);

        if (!success)
            return BadRequest("Registration failed. Username may already be taken or input is invalid.");

        return Ok("User registered successfully.");
    }
}