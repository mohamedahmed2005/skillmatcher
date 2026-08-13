using SkillMatch.API.Core.Entities.Base;

namespace SkillMatch.API.Core.Entities;

public class Skill : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
