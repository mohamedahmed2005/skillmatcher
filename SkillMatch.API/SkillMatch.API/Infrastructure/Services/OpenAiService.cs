using SkillMatch.API.Core.DTOs.Matching;
using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class OpenAiService : IAiMatchingService
{
    public Task<MatchResultDto> AnalyzeAsync(string cvText, string jobDescription) => throw new NotImplementedException();
    public Task<IEnumerable<RecommendedJobDto>> GetRecommendedJobsAsync(int candidateId) => throw new NotImplementedException();
}
