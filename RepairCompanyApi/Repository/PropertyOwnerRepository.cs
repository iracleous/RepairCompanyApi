using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository;

public class PropertyOwnerRepository: IPropertyOwnerRepository
{
    private readonly RepairDbContext _context;
    private readonly ILogger<PropertyOwner> _logger;

    public PropertyOwnerRepository(RepairDbContext context, ILogger<PropertyOwner> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PropertyOwner>> GetAllAsync(int pageCount, int pageSize)
    {
        return await _context.PropertyOwners.Include(o => o.BuildingProperties).ToListAsync();
    }
   
    public async Task<PropertyOwner?> GetByIdAsync(long id)
    {
        return await _context.PropertyOwners.FindAsync(id);
    }

    public async Task<bool> AddAsync(PropertyOwner PropertyOwner)
    {
        await _context.PropertyOwners.AddAsync(PropertyOwner);
        var result = await _context.SaveChangesAsync();
        return result > 0;
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
            _logger.LogWarning("Database update failed: {message}",ex.Message);
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
   
}
