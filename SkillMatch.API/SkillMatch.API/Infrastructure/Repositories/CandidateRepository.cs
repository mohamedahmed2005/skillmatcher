using SkillMatch.API.Core.Entities;
using SkillMatch.API.Core.Interfaces.Repositories;
using SkillMatch.API.Infrastructure.Data;

namespace SkillMatch.API.Infrastructure.Repositories;

public class CandidateRepository : GenericRepository<CandidateProfile>, ICandidateRepository
{
    public CandidateRepository(ApplicationDbContext context) : base(context) { }
}
