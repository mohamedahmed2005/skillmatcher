using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class AzureBlobStorageService : IFileStorageService
{
    public Task<string> SaveAsync(IFormFile file, string folder) => throw new NotImplementedException();
    public Task DeleteAsync(string filePath) => throw new NotImplementedException();
}
