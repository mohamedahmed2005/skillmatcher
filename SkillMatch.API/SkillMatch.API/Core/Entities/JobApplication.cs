using SkillMatch.API.Core.Entities.Base;
using SkillMatch.API.Core.Enums;

namespace SkillMatch.API.Core.Entities;

public class JobApplication : BaseEntity
{
    public int CandidateProfileId { get; set; }
    public CandidateProfile Candidate { get; set; } = null!;
    public int JobPostingId { get; set; }
    public JobPosting JobPosting { get; set; } = null!;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public double? MatchScore { get; set; }
}
