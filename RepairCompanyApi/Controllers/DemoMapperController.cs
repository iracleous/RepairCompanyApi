﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Services;

namespace RepairCompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DemoMapperController : ControllerBase
{
    private readonly PropertyOwnerService2 _propertyOwnerService2;

    public DemoMapperController(PropertyOwnerService2 propertyOwnerService2)
    {
        _propertyOwnerService2 = propertyOwnerService2;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<OwnerData>>> GetOwnerData()
    {
        return await _propertyOwnerService2.GetOwnerData(1, 1);
    }
}
