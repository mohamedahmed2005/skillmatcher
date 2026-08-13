using SkillMatch.API.Core.Entities.Base;
using SkillMatch.API.Core.Enums;

namespace SkillMatch.API.Core.Entities;

public class JobPosting : BaseEntity
{
    public int CompanyProfileId { get; set; }
    public CompanyProfile Company { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public ExperienceLevel ExperienceLevel { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public ICollection<Skill> RequiredSkills { get; set; } = [];
    public ICollection<JobApplication> Applications { get; set; } = [];
}
