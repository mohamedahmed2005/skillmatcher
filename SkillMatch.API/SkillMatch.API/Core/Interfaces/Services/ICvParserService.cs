namespace SkillMatch.API.Core.Interfaces.Services;

public interface ICvParserService
{
    Task<string> ExtractTextAsync(Stream fileStream);
    Task<IEnumerable<string>> ExtractSkillsAsync(string cvText);
}
