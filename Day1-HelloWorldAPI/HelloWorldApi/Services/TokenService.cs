using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelloWorldApi.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken (string username)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var jwtKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
        var key = new SymmetricSecurityKey(jwtKey);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
            signingCredentials: creds
            );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}