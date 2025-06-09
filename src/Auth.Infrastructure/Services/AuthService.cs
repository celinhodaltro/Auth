using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IAuthRepository _authRepository;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(IConfiguration config, IAuthRepository authRepository)
    {
        _config = config;
        _authRepository = authRepository;
    }

    public async Task<bool> RegisterAsync(string username, string password)
    {
        var exists = await _authRepository.ExistsAsync(username);
        if (exists) return false;

        var user = new User { Username = username };
        user.PasswordHash = _hasher.HashPassword(user, password);

        await _authRepository.AddUserAsync(user);
        return true;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _authRepository.GetByEmailAsync(username);
        if (user == null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed) return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
