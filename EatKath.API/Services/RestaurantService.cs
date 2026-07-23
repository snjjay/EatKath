using AutoMapper;
using AutoMapper.QueryableExtensions;
using EatKath.API.Data;
using EatKath.API.DTOs.Restaurant;
using EatKath.API.Entities;
using EatKath.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EatKath.API.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Area)
                .ProjectTo<RestaurantDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<RestaurantDto?> GetByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Area)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
                return null;

            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            await _context.Entry(restaurant)
                .Reference(r => r.Area)
                .LoadAsync();

            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto?> UpdateAsync(int id, UpdateRestaurantDto dto)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Area)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
                return null;

            _mapper.Map(dto, restaurant);

            await _context.SaveChangesAsync();

            await _context.Entry(restaurant)
                .Reference(r => r.Area)
                .LoadAsync();

            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
                return false;

            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}