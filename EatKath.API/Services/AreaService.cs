using AutoMapper;
using EatKath.API.Data;
using EatKath.API.DTOs.Area;
using EatKath.API.Entities;
using EatKath.API.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EatKath.API.Exceptions;

namespace EatKath.API.Services;

public class AreaService : IAreaService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAreaDto> _createValidator;
    private readonly IValidator<UpdateAreaDto> _updateValidator;

    public AreaService(
    ApplicationDbContext context,
    IMapper mapper,
    IValidator<CreateAreaDto> createValidator,
    IValidator<UpdateAreaDto> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<AreaDto>> GetAllAsync()
    {
        var areas = await _context.Areas.ToListAsync();

        return _mapper.Map<IEnumerable<AreaDto>>(areas);
    }

    public async Task<AreaDto?> GetByIdAsync(int id)
    {
        var area = await _context.Areas.FindAsync(id);

        if (area == null)
        {
            return null;
        }

        return _mapper.Map<AreaDto>(area);
    }




    public async Task<AreaDto> CreateAsync(CreateAreaDto dto)
    {
        // Validate request
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Check for duplicate area
        var exists = await _context.Areas
            .AnyAsync(a => a.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Area already exists.");
        }

        // Create entity
        var area = _mapper.Map<Area>(dto);

        _context.Areas.Add(area);

        await _context.SaveChangesAsync();

        return _mapper.Map<AreaDto>(area);
    }

    public async Task<AreaDto?> UpdateAsync(int id, UpdateAreaDto dto)
    {
        // Validate request
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Find existing area
        var area = await _context.Areas.FindAsync(id);

        if (area == null)
        {
            return null;
        }

        // Check for duplicate name (excluding current record)
        var exists = await _context.Areas.AnyAsync(a =>
            a.Id != id &&
            a.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Area already exists.");
        }

        // Update entity
        _mapper.Map(dto, area);

        await _context.SaveChangesAsync();

        return _mapper.Map<AreaDto>(area);
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var area = await _context.Areas.FindAsync(id);

        if (area == null)
        {
            return false;
        }

        _context.Areas.Remove(area);

        await _context.SaveChangesAsync();

        return true;
    }


}