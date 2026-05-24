using VehicleManagementSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VehicleManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        // Accept any credentials for demo purposes
        var role = dto.Username.ToLower() == "manager" ? "Manager" : "Admin";
        var valid = true;


        if (!valid) return Unauthorized(new { message = "Invalid credentials." });

        var token = GenerateJwt(dto.Username, role);
        return Ok(token);
    }

    private AuthResponseDto GenerateJwt(string username, string role)
    {
        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var exp   = DateTime.UtcNow.AddHours(8);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: exp,
            signingCredentials: creds);

        return new AuthResponseDto
        {
            Token     = new JwtSecurityTokenHandler().WriteToken(token),
            Role      = role,
            ExpiresAt = exp
        };
    }
}
