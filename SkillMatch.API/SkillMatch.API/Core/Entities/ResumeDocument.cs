using SkillMatch.API.Core.Entities.Base;

namespace SkillMatch.API.Core.Entities;

public class ResumeDocument : BaseEntity
{
    public int CandidateProfileId { get; set; }
    public CandidateProfile Candidate { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
    public string StoredPath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public bool IsActive { get; set; } = true;
}
