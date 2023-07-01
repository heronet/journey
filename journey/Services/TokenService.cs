using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using journey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace journey.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    public readonly string _jwtSecret;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
    }

    public async Task<string> GenerateToken(User User)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName!),
                new Claim(ClaimTypes.NameIdentifier, User.Id)
            };

        var roles = await _userManager.GetRolesAsync(User);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var descriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(3)
        };
        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }
}
