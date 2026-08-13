using SkillMatch.API.Core.DTOs.Auth;
using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<AuthResponseDto> RegisterAsync(RegisterDto dto) => throw new NotImplementedException();
    public Task<AuthResponseDto> LoginAsync(LoginDto dto) => throw new NotImplementedException();
    public Task<AuthResponseDto> RefreshTokenAsync(string refreshToken) => throw new NotImplementedException();
    public Task RevokeTokenAsync(string refreshToken) => throw new NotImplementedException();
}
