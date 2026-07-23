using AutoMapper;
using EatKath.API.DTOs.Restaurant;
using EatKath.API.Entities;

namespace EatKath.API.Mappings
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            // Entity -> DTO
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.AreaName,
                           opt => opt.MapFrom(src => src.Area.Name));

            // Create DTO -> Entity
            CreateMap<CreateRestaurantDto, Restaurant>();

            // Update DTO -> Entity
            CreateMap<UpdateRestaurantDto, Restaurant>();
        }
    }
}