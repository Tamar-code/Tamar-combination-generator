using AutoMapper;
using CombinationGeneratorAPI.Application.DTOs;
using CombinationGeneratorAPI.Application.Queries;
namespace CombinationGeneratorAPI.Application.Mapping;
public class CombinationMappingProfile : Profile
{
    public CombinationMappingProfile()
    {
        CreateMap<CombinationRequest, GetCombinationsQuery>();
    }
}
