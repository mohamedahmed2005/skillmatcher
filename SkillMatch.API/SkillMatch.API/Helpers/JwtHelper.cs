using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SkillMatch.API.Core.Entities;

namespace SkillMatch.API.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateAccessToken(ApplicationUser user) => throw new NotImplementedException();
    public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    public ClaimsPrincipal? ValidateToken(string token) => throw new NotImplementedException();
}
