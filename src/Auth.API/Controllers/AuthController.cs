using Auth.Application.Interfaces;
using Auth.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequest request)
    {
        var result = await _authService.RegisterAsync(request.Username, request.Password);
        return result ? Ok("Registered") : BadRequest("User already exists");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Username, request.Password);
        if (token == null) return Unauthorized();
        return Ok(new { Token = token });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(User.Identity?.Name);
    }
}
