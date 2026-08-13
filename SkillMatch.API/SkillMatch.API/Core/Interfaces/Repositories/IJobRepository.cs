using SkillMatch.API.Core.Entities;

namespace SkillMatch.API.Core.Interfaces.Repositories;

public interface IJobRepository : IGenericRepository<JobPosting>
{
    Task<IEnumerable<JobPosting>> GetBySkillsAsync(IEnumerable<int> skillIds);
}
