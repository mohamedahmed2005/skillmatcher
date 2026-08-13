namespace SkillMatch.API.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICandidateRepository Candidates { get; }
    IJobRepository Jobs { get; }
    Task<int> CompleteAsync();
}
