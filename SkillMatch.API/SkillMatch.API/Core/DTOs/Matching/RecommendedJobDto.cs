namespace SkillMatch.API.Core.DTOs.Matching;

public class RecommendedJobDto
{
    public int JobId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public double MatchScore { get; set; }
}
