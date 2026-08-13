using SkillMatch.API.Core.DTOs.Candidates;

namespace SkillMatch.API.Core.Interfaces.Services;

public interface ICandidateService
{
    Task<CandidateProfileDto> GetProfileAsync(int userId);
    Task<CandidateProfileDto> UpdateProfileAsync(int userId, UpdateProfileDto dto);
}
