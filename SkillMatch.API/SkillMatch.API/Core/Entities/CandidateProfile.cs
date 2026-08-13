using SkillMatch.API.Core.Entities.Base;

namespace SkillMatch.API.Core.Entities;

public class CandidateProfile : BaseEntity
{
    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public ICollection<ResumeDocument> Resumes { get; set; } = [];
}
