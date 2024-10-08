﻿using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository.Implenentations;

public class PropertyOwnerRepositoryUsingEF : IPropertyOwnerRepository
{
    private readonly RepairDbContext _context;
    private readonly ILogger<PropertyOwner> _logger;

    public PropertyOwnerRepositoryUsingEF(RepairDbContext context, ILogger<PropertyOwner> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PropertyOwner>> GetAllAsync(int pageCount, int pageSize)
    {
        try
        {
            return await _context.PropertyOwners
                .Include(o => o.BuildingProperties)
                .ThenInclude(b => b.Repairs)
                .Skip(pageSize * (pageCount - 1))
                .Take(pageSize)
                .ToListAsync();
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<PropertyOwner?> GetByIdAsync(long id)
    {
        return await _context.PropertyOwners.FindAsync(id);
    }

    public async Task<long> AddAsync(PropertyOwner PropertyOwner)
    {
        await _context.PropertyOwners.AddAsync(PropertyOwner);
        await _context.SaveChangesAsync();
        return PropertyOwner.Id;
    }

    public async Task<bool> UpdateAsync(PropertyOwner owner)
    {
        try
        {
            _context.PropertyOwners.Update(owner);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning("Concurrency issue occurred: {message}", ex.Message);
            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning("Database update failed: {message}", ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning("An error occurred: {message}", ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var PropertyOwner = await _context.PropertyOwners.FindAsync(id);
        if (PropertyOwner != null)
        {
            _context.PropertyOwners.Remove(PropertyOwner);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        return false;
    }

    public async Task<PropertyOwner?> GetByNameAsync(string name)
    {
        return await _context
            .PropertyOwners
            .FirstOrDefaultAsync(o => o.LastName == name);
    }


    public async Task<ApiResult<IEnumerable<PropertyOwner>>> GetAllAsync2(int pageCount, int pageSize)
    {
        try
        {
            return new ApiResult<IEnumerable<PropertyOwner>>
            {
                StatusCode = 0,
                Description = string.Empty,
                Result = await _context.PropertyOwners
                                    .Include(o => o.BuildingProperties)
                                    .ThenInclude(b => b.Repairs)
                                    .Skip(pageSize * (pageCount - 1))
                                    .Take(pageSize)
                                    .ToListAsync()
            };
        }
        catch (ArgumentException ea)
        {
            return new ApiResult<IEnumerable<PropertyOwner>>
            {
                StatusCode = 12,
                Description = ea.Message,
                Result = null
            };
        }
        catch (Exception e)
        {
            return new ApiResult<IEnumerable<PropertyOwner>>
            {
                StatusCode = 10,
                Description = e.Message,
                Result = null
            };
        }
    }
}
