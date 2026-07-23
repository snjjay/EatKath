using EatKath.API.DTOs.Area;
using EatKath.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EatKath.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreasController : ControllerBase
{
    private readonly IAreaService _areaService;

    public AreasController(IAreaService areaService)
    {
        _areaService = areaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var areas = await _areaService.GetAllAsync();

        return Ok(areas);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var area = await _areaService.GetByIdAsync(id);

        if (area == null)
        {
            return NotFound();
        }

        return Ok(area);
    }



    [HttpPost]
    public async Task<IActionResult> Create(CreateAreaDto dto)
    {
        var area = await _areaService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetAll), new { id = area.Id }, area);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAreaDto dto)
    {
        var area = await _areaService.UpdateAsync(id, dto);

        if (area == null)
        {
            return NotFound();
        }

        return Ok(area);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _areaService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }


}