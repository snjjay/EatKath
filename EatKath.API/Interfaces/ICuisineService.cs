using EatKath.API.DTOs.Cuisine;

namespace EatKath.API.Interfaces
{
    public interface ICuisineService
    {
        Task<IEnumerable<CuisineDto>> GetAllAsync();
        Task<CuisineDto?> GetByIdAsync(int id);
        Task<CuisineDto> CreateAsync(CreateCuisineDto dto);
        Task<CuisineDto?> UpdateAsync(int id, UpdateCuisineDto dto);
        Task<bool> DeleteAsync(int id);
    }
}