namespace SkillMatch.API.Core.Interfaces.Services;

public interface IFileStorageService
{
    Task<string> SaveAsync(IFormFile file, string folder);
    Task DeleteAsync(string filePath);
}
