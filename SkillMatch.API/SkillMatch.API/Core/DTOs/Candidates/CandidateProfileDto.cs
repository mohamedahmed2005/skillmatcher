namespace SkillMatch.API.Core.DTOs.Candidates;

public class CandidateProfileDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public bool HasActiveResume { get; set; }
}
