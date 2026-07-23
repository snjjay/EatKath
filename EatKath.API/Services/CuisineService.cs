using AutoMapper;
using EatKath.API.Data;
using EatKath.API.DTOs.Cuisine;
using EatKath.API.Entities;
using EatKath.API.Exceptions;
using EatKath.API.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EatKath.API.Services;

public class CuisineService : ICuisineService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCuisineDto> _createValidator;
    private readonly IValidator<UpdateCuisineDto> _updateValidator;

    public CuisineService(
        ApplicationDbContext context,
        IMapper mapper,
        IValidator<CreateCuisineDto> createValidator,
        IValidator<UpdateCuisineDto> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<CuisineDto>> GetAllAsync()
    {
        var cuisines = await _context.Cuisines.ToListAsync();

        return _mapper.Map<IEnumerable<CuisineDto>>(cuisines);
    }

    public async Task<CuisineDto?> GetByIdAsync(int id)
    {
        var cuisine = await _context.Cuisines.FindAsync(id);

        if (cuisine == null)
        {
            return null;
        }

        return _mapper.Map<CuisineDto>(cuisine);
    }

    public async Task<CuisineDto> CreateAsync(CreateCuisineDto dto)
    {
        // Validate request
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Check for duplicate cuisine
        var exists = await _context.Cuisines
            .AnyAsync(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Cuisine already exists.");
        }

        // Create entity
        var cuisine = _mapper.Map<Cuisine>(dto);

        _context.Cuisines.Add(cuisine);

        await _context.SaveChangesAsync();

        return _mapper.Map<CuisineDto>(cuisine);
    }

    public async Task<CuisineDto?> UpdateAsync(int id, UpdateCuisineDto dto)
    {
        // Validate request
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Find existing cuisine
        var cuisine = await _context.Cuisines.FindAsync(id);

        if (cuisine == null)
        {
            return null;
        }

        // Check for duplicate name (excluding current record)
        var exists = await _context.Cuisines.AnyAsync(c =>
            c.Id != id &&
            c.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Cuisine already exists.");
        }

        // Update entity
        _mapper.Map(dto, cuisine);

        await _context.SaveChangesAsync();

        return _mapper.Map<CuisineDto>(cuisine);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cuisine = await _context.Cuisines.FindAsync(id);

        if (cuisine == null)
        {
            return false;
        }

        _context.Cuisines.Remove(cuisine);

        await _context.SaveChangesAsync();

        return true;
    }
}