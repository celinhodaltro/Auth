using Auth.Application.Commands;
using Auth.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        bool isAuthenticated = await _mediator.Send(request);

        if (!isAuthenticated) 
            return Unauthorized();

        return Ok(new { isAuthenticated });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(User.Identity?.Name);
    }
}
