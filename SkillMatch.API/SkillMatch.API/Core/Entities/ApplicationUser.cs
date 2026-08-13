using SkillMatch.API.Core.Entities.Base;
using SkillMatch.API.Core.Enums;

namespace SkillMatch.API.Core.Entities;

public class ApplicationUser : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
