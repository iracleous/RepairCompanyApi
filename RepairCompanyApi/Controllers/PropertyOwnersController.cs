using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Data;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Services;

namespace RepairCompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyOwnersController : ControllerBase
{
    private readonly IPropertyOwnerService _service;
    private readonly ILogger<PropertyOwnersController> _logger;

    public PropertyOwnersController(IPropertyOwnerService service, ILogger<PropertyOwnersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/PropertyOwners
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(
       [FromQuery] int pageCount, [FromQuery] int pageSize)
    {
        _logger.LogDebug("Started");
        return await _service.GetPropertyOwners(pageCount, pageSize);
    }

    // GET: api/PropertyOwners/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDataDto?>> GetPropertyOwner(long id)
    {
        return await _service.GetPropertyOwner(id);
    }

    // PUT: api/PropertyOwners/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner)
    {
          return await _service.PutPropertyOwner(id, propertyOwner);
    }

    // POST: api/PropertyOwners
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner)
    {
        return await _service.PostPropertyOwner(propertyOwner);
     }

    // DELETE: api/PropertyOwners/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropertyOwner(long id)
    {
        return await _service.DeletePropertyOwner(id);
    }



    /////
    [HttpPut("building")]
    public async Task<IActionResult> AssignProperty([FromQuery] long propertyOwnerId,
        [FromQuery] long buildingId)
    {
        return await _service.AssignPropertyToOwner(propertyOwnerId, buildingId);
    }

    ///
    [HttpGet("owners")]
    public async Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwners(
       [FromQuery] int pageCount, [FromQuery] int pageSize)
    {
        _logger.LogDebug("Started");

        var timeStart = DateTime.Now;

         var result = await _service.GetOwnerDataAsync(pageCount, pageSize);

        var timeEnd = DateTime.Now;
        var timeElapsed = timeEnd - timeStart;
        Console.WriteLine(timeElapsed);
        return result;

    }


    [HttpGet("stats")]
    public async Task<ActionResult<Statistics>> GetStatistics()
    {
        return await _service.CalculateStatistics();
    }








}
