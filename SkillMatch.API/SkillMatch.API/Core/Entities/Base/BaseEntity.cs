using SkillMatch.API.Core.Interfaces.Repositories;

namespace SkillMatch.API.Core.Entities.Base;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
