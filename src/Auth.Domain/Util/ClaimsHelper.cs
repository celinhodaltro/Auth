using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Server.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Domain.Util
{

    public static class ClaimsHelper
    {
        public static UserToken BuildToken(this IConfiguration configuration, User user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("AppMain", "UrsullaOnline.com.br"),
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, configuration["Jwt:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, configuration["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            DateTime expiration = DateTime.UtcNow.AddHours(2);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
