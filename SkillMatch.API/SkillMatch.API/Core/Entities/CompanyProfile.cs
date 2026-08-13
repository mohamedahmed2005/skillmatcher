using SkillMatch.API.Core.Entities.Base;

namespace SkillMatch.API.Core.Entities;

public class CompanyProfile : BaseEntity
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public string CompanyName { get; set; } = string.Empty;
    public string? Website { get; set; }
    public ICollection<JobPosting> JobPostings { get; set; } = [];
}
