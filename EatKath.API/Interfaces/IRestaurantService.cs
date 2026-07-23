using EatKath.API.DTOs.Restaurant;

namespace EatKath.API.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllAsync();

        Task<RestaurantDto?> GetByIdAsync(int id);

        Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto);

        Task<RestaurantDto?> UpdateAsync(int id, UpdateRestaurantDto dto);

        Task<bool> DeleteAsync(int id);
    }
}