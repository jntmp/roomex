// MyMappingProfile.cs
using Api.Models;
using AutoMapper;

namespace Api;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
				// not entirely necessary, just for showcase purposes
        CreateMap<CalculateDistanceRequest, LatLongPairDto>();
				CreateMap<DistanceWithUnitDto, CalculateDistanceResponse>();
    }
}