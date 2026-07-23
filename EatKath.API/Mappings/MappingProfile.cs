using AutoMapper;
using EatKath.API.DTOs.Area;
using EatKath.API.Entities;

namespace EatKath.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Area, AreaDto>();

        CreateMap<CreateAreaDto, Area>();

        CreateMap<UpdateAreaDto, Area>();
    }
}