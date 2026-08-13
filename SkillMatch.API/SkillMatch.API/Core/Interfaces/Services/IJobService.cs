using SkillMatch.API.Core.DTOs.Jobs;

namespace SkillMatch.API.Core.Interfaces.Services;

public interface IJobService
{
    Task<JobResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<JobResponseDto>> GetAllAsync();
    Task<JobResponseDto> CreateAsync(CreateJobDto dto);
    Task<JobResponseDto> UpdateAsync(int id, UpdateJobDto dto);
    Task DeleteAsync(int id);
}
