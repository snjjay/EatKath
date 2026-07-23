using EatKath.API.DTOs.DiningType;

namespace EatKath.API.Interfaces
{
    public interface IDiningTypeService
    {
        Task<IEnumerable<DiningTypeDto>> GetAllAsync();
        Task<DiningTypeDto?> GetByIdAsync(int id);
        Task<DiningTypeDto> CreateAsync(CreateDiningTypeDto dto);
        Task<DiningTypeDto?> UpdateAsync(int id, UpdateDiningTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}