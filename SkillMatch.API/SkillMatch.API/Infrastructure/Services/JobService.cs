using SkillMatch.API.Core.DTOs.Jobs;
using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class JobService : IJobService
{
    public Task<JobResponseDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<JobResponseDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<JobResponseDto> CreateAsync(CreateJobDto dto) => throw new NotImplementedException();
    public Task<JobResponseDto> UpdateAsync(int id, UpdateJobDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}
