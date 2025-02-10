using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelloWorldApi.Models;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly JwtSettings _jwt;
    
    public AuthService(AppDbContext db,JwtSettings jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public string GenerateToken (User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: new[] { new Claim(ClaimTypes.Name, user.Username) },
            expires: DateTime.Now.AddMinutes(_jwt.ExpiryInMinutes),
            signingCredentials: creds
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}