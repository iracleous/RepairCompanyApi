﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services
{
    public class PropertyOwnerService : IPropertyOwnerService
    {
        private readonly RepairDbContext _context;
        private ILogger<PropertyOwnerService> _logger;

        public PropertyOwnerService(RepairDbContext context, ILogger<PropertyOwnerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize)
        {
            _logger.LogDebug("start");
            if (pageCount <= 0) pageCount = 1;
            if (pageSize <= 0 || pageSize>20) pageSize = 10;
            return await _context.PropertyOwners
                .Skip( (pageCount-1)*pageSize )
                .Take(pageSize)
                .ToListAsync();
        }

       
        public async Task<ActionResult<PropertyOwner>> GetPropertyOwner(long id)
        {
            var propertyOwner = await _context
                .PropertyOwners
                .Include(o => o.BuildingProperties)
                .FirstOrDefaultAsync(o => o.Id == id);
             

            if (propertyOwner == null)
            {
                return   new NotFoundResult();
            }

            return propertyOwner;
        }

       
        public async Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner)
        {
            if (id != propertyOwner.Id)
            {
                return new BadRequestResult();
            }

            _context.Entry(propertyOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyOwnerExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new NoContentResult();
        }

         
        public async Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner)
        {
            _context.PropertyOwners.Add(propertyOwner);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult(
                "GetPropertyOwner", null, new { id = propertyOwner.Id }, propertyOwner );
        }

        // DELETE: api/PropertyOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyOwner(long id)
        {
            var propertyOwner = await _context.PropertyOwners.FindAsync(id);
            if (propertyOwner == null)
            {
                return new NotFoundResult();
            }

            _context.PropertyOwners.Remove(propertyOwner);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool PropertyOwnerExists(long id)
        {
            return _context.PropertyOwners.Any(e => e.Id == id);
        }
    }
}

