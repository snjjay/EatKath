using EatKath.API.DTOs.Restaurant;
using EatKath.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EatKath.API.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateRestaurantDto dto)
        {
            var restaurant = await _restaurantService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = restaurant.Id },
                restaurant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = await _restaurantService.UpdateAsync(id, dto);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _restaurantService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}