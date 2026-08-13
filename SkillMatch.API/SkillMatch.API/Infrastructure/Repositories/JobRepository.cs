using Microsoft.EntityFrameworkCore;
using SkillMatch.API.Core.Entities;
using SkillMatch.API.Core.Interfaces.Repositories;
using SkillMatch.API.Infrastructure.Data;

namespace SkillMatch.API.Infrastructure.Repositories;

public class JobRepository : GenericRepository<JobPosting>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<JobPosting>> GetBySkillsAsync(IEnumerable<int> skillIds) =>
        await _dbSet
            .Where(j => j.RequiredSkills.Any(s => skillIds.Contains(s.Id)))
            .ToListAsync();
}
