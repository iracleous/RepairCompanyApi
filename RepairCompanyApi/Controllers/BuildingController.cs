using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildingController : ControllerBase
{

    private readonly RepairDbContext _context;

    public BuildingController(RepairDbContext context)
    {
        _context = context;
    }



    [HttpPost]
    public async Task<ActionResult<BuildingProperty>> AddBuildingProperty(BuildingDto buildingDto)
    {
        
        PropertyOwner? propertyOwner = await _context.PropertyOwners.FindAsync(
            buildingDto.PropertyOwnerId);

        if (propertyOwner == null)
        {
            return NotFound();
        }

        BuildingProperty property = new BuildingProperty();
        property.PropertyOwner = propertyOwner;
        property.Address = buildingDto.Address;
        
        _context.BuildingProperties.Add(property);
        await _context.SaveChangesAsync();
        return Ok(property);

    }


    [HttpGet("stats")]
    public async Task<ActionResult<Statistics>> GetStatistics()
    {
        var statistics = new Statistics();
        statistics.NumberOfPropertyOwners =
           await  _context.PropertyOwners.CountAsync();
        statistics.NumberOfOwnersWithZeroProperties =
           await _context
               .PropertyOwners
               .CountAsync(propOwn => propOwn.BuildingProperties.Count() == 0);
        return statistics;
    }


}
