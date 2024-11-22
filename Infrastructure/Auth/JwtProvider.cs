using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces.Services.Auth;
using Core.JWT;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
    public string GenerateToken(string id, string name, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, id),
            new (JwtRegisteredClaimNames.Name, name),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            JwtOptions.Issuer,
            JwtOptions.Audience,
            claims,
            null,
            DateTime.Now.AddMinutes(JwtOptions.ExpirationTime),
            signInCredentials
        );
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}