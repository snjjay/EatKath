using EatKath.API.DTOs.DiningType;
using EatKath.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EatKath.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiningTypeController : ControllerBase
{
    private readonly IDiningTypeService _diningTypeService;

    public DiningTypeController(IDiningTypeService diningTypeService)
    {
        _diningTypeService = diningTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var diningTypes = await _diningTypeService.GetAllAsync();
        return Ok(diningTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var diningType = await _diningTypeService.GetByIdAsync(id);

        if (diningType == null)
        {
            return NotFound();
        }

        return Ok(diningType);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDiningTypeDto dto)
    {
        var createdDiningType = await _diningTypeService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdDiningType.Id },
            createdDiningType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDiningTypeDto dto)
    {
        var updatedDiningType = await _diningTypeService.UpdateAsync(id, dto);

        if (updatedDiningType == null)
        {
            return NotFound();
        }

        return Ok(updatedDiningType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _diningTypeService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}