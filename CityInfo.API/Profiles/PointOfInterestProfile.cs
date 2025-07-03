using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles;

public class PointOfInterestProfile : Profile
{
    public PointOfInterestProfile()
    {
        CreateMap<Entities.PointOfInterest, PointOfInterestDto>();
        CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
        CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
        CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
    }
}