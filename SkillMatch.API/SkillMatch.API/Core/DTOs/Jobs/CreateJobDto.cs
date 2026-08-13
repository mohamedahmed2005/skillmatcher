namespace SkillMatch.API.Core.DTOs.Jobs;

public class CreateJobDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string ExperienceLevel { get; set; } = string.Empty;
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public List<int> SkillIds { get; set; } = [];
}
