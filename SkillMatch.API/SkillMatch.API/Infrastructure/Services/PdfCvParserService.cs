using SkillMatch.API.Core.Interfaces.Services;

namespace SkillMatch.API.Infrastructure.Services;

public class PdfCvParserService : ICvParserService
{
    public Task<string> ExtractTextAsync(Stream fileStream) => throw new NotImplementedException();
    public Task<IEnumerable<string>> ExtractSkillsAsync(string cvText) => throw new NotImplementedException();
}
