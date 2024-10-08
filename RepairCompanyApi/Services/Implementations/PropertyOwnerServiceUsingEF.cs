﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using System.Drawing.Printing;

namespace RepairCompanyApi.Services.Implementations
{
    public class PropertyOwnerServiceUsingEF : IPropertyOwnerService
    {
        private readonly RepairDbContext _context;
        private ILogger<PropertyOwnerServiceUsingEF> _logger;
        private readonly IMapper _mapper;

        public PropertyOwnerServiceUsingEF(RepairDbContext context, ILogger<PropertyOwnerServiceUsingEF> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize)
        {
            _logger.LogDebug("start");
            if (pageCount <= 0) pageCount = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 10;
            return await _context.PropertyOwners.Include(o => o.BuildingProperties)
                .Skip((pageCount - 1) * pageSize)
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
                return new NotFoundResult();
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

     public async    Task<ApiResult<long>> CreatePropertyOwner(PropertyOwnerDtoRequest propertyOwnerDto) 
        {
            var propertyOwner = _mapper.Map<PropertyOwner>(propertyOwnerDto);
            _context.PropertyOwners.Add(propertyOwner);
            await _context.SaveChangesAsync();

            return new ApiResult<long>
            {
                Description = "Successfully saved",
                 StatusCode =0,
                  Result = propertyOwner.Id
            };
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

        public async Task<IActionResult> AssignPropertyToOwner(long propertyOwnerId, long propertyId)
        {
            PropertyOwner? propertyOwner = await _context.PropertyOwners
                .FindAsync(propertyOwnerId);
            if (propertyOwner == null)
            {
                return new NotFoundResult();
            }
            BuildingProperty? buildingProperty = await _context.BuildingProperties
                .FindAsync(propertyId);
            if (buildingProperty == null)
            {
                return new NotFoundResult();
            }
            buildingProperty.PropertyOwner = propertyOwner;
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerDataAsync(int pageCount, int pageSize)
        {
            _logger.LogDebug("start");
            if (pageCount <= 0) pageCount = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 10;

            List<PropertyOwner> propertyOwners = await _context.PropertyOwners.Include(o => o.BuildingProperties)
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<OwnerDataDto> result = propertyOwners
                        .ConvertAll(o => new OwnerDataDto
                        {
                            OwnerId = o.Id,
                            OwnerName = o.LastName + " " + o.FirstName,
                            Buildings = o.BuildingProperties.ConvertAll(
                                 b => new BuildingOwnerDto
                                 {
                                     Address = b.Address,
                                     Id = b.Id
                                 }
                                 )
                        }
            );
            return result;

            //return await _context
            //    .PropertyOwners
            //    .Include(o => o.BuildingProperties)
            //    .Skip((pageCount - 1) * pageSize)
            //    .Take(pageSize)

            //    .Select(o => new OwnerData
            //    {
            //        OwnerId = o.Id,
            //        OwnerName = o.LastName + " " + o.FirstName,
            //        Buildings = o.BuildingProperties.ConvertAll<BuildingOwnerDto>(
            //                         b => new BuildingOwnerDto
            //                         {
            //                             Address = b.Address,
            //                             BuildingId = b.Id
            //                         }
            //                         )
            //    }
            //    )
            //    .ToListAsync();
        }

        public async Task<ActionResult<Statistics>> CalculateStatistics()
        {
            var statistics = new Statistics
            {
                NumberOfPropertyOwners =
               await _context.PropertyOwners.CountAsync(),
                NumberOfOwnersWithZeroProperties =
               await _context
                   .PropertyOwners
                   .CountAsync(propOwn => propOwn.BuildingProperties.Count() == 0)
            };
            return statistics; ;
        }

        Task<ActionResult<OwnerDataDto?>> IPropertyOwnerService.GetPropertyOwner(long id)
        {
            throw new NotImplementedException();
        }

       
    }
}

