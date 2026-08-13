using SkillMatch.API.Core.DTOs.Matching;

namespace SkillMatch.API.Core.Interfaces.Services;

public interface IAiMatchingService
{
    Task<MatchResultDto> AnalyzeAsync(string cvText, string jobDescription);
    Task<IEnumerable<RecommendedJobDto>> GetRecommendedJobsAsync(int candidateId);
}
