using AutoMapper;
using SkillMatch.API.Core.DTOs.Jobs;
using SkillMatch.API.Core.Entities;

namespace SkillMatch.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<JobPosting, JobResponseDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
            .ForMember(dest => dest.RequiredSkills, opt => opt.MapFrom(src => src.RequiredSkills.Select(s => s.Name)));

        CreateMap<CreateJobDto, JobPosting>();
    }
}
