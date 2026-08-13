using SkillMatch.API.Core.DTOs.Candidates;
using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class CandidateService : ICandidateService
{
    public Task<CandidateProfileDto> GetProfileAsync(int userId) => throw new NotImplementedException();
    public Task<CandidateProfileDto> UpdateProfileAsync(int userId, UpdateProfileDto dto) => throw new NotImplementedException();
}
