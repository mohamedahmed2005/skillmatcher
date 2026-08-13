namespace SkillMatch.API.Core.DTOs.Matching;

public class MatchResultDto
{
    public int JobId { get; set; }
    public double MatchScore { get; set; }
    public string Summary { get; set; } = string.Empty;
    public List<string> MatchedSkills { get; set; } = [];
    public List<string> MissingSkills { get; set; } = [];
}
