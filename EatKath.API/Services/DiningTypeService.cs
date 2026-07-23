using AutoMapper;
using EatKath.API.Data;
using EatKath.API.DTOs.DiningType;
using EatKath.API.Entities;
using EatKath.API.Exceptions;
using EatKath.API.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EatKath.API.Services;

public class DiningTypeService : IDiningTypeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateDiningTypeDto> _createValidator;
    private readonly IValidator<UpdateDiningTypeDto> _updateValidator;

    public DiningTypeService(
        ApplicationDbContext context,
        IMapper mapper,
        IValidator<CreateDiningTypeDto> createValidator,
        IValidator<UpdateDiningTypeDto> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IEnumerable<DiningTypeDto>> GetAllAsync()
    {
        var diningTypes = await _context.DiningTypes.ToListAsync();

        return _mapper.Map<IEnumerable<DiningTypeDto>>(diningTypes);
    }

    public async Task<DiningTypeDto?> GetByIdAsync(int id)
    {
        var diningType = await _context.DiningTypes.FindAsync(id);

        if (diningType == null)
        {
            return null;
        }

        return _mapper.Map<DiningTypeDto>(diningType);
    }

    public async Task<DiningTypeDto> CreateAsync(CreateDiningTypeDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var exists = await _context.DiningTypes
            .AnyAsync(d => d.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Dining type already exists.");
        }

        var diningType = _mapper.Map<DiningType>(dto);

        _context.DiningTypes.Add(diningType);

        await _context.SaveChangesAsync();

        return _mapper.Map<DiningTypeDto>(diningType);
    }

    public async Task<DiningTypeDto?> UpdateAsync(int id, UpdateDiningTypeDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var diningType = await _context.DiningTypes.FindAsync(id);

        if (diningType == null)
        {
            return null;
        }

        var exists = await _context.DiningTypes.AnyAsync(d =>
            d.Id != id &&
            d.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

        if (exists)
        {
            throw new DuplicateEntityException("Dining type already exists.");
        }

        _mapper.Map(dto, diningType);

        await _context.SaveChangesAsync();

        return _mapper.Map<DiningTypeDto>(diningType);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var diningType = await _context.DiningTypes.FindAsync(id);

        if (diningType == null)
        {
            return false;
        }

        _context.DiningTypes.Remove(diningType);

        await _context.SaveChangesAsync();

        return true;
    }
}