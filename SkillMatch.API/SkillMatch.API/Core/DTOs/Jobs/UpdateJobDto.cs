namespace SkillMatch.API.Core.DTOs.Jobs;

public class UpdateJobDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? ExperienceLevel { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public List<int>? SkillIds { get; set; }
}
