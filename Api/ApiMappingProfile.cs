// MyMappingProfile.cs
using Api.Models;
using AutoMapper;

namespace Api;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<CalculateDistanceRequest, LatLongPairDto>();
				CreateMap<DistanceWithUnitDto, CalculateDistanceResponse>();
    }
}