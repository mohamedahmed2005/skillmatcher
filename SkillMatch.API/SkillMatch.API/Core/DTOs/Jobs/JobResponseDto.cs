namespace SkillMatch.API.Core.DTOs.Jobs;

public class JobResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string ExperienceLevel { get; set; } = string.Empty;
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public List<string> RequiredSkills { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
