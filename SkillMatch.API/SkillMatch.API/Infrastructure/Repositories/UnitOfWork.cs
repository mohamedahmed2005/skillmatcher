using SkillMatch.API.Core.Interfaces.Repositories;
using SkillMatch.API.Infrastructure.Data;

namespace SkillMatch.API.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public ICandidateRepository Candidates { get; }
    public IJobRepository Jobs { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Candidates = new CandidateRepository(context);
        Jobs = new JobRepository(context);
    }

    public async Task<int> CompleteAsync() =>
        await _context.SaveChangesAsync();

    public void Dispose() =>
        _context.Dispose();
}
