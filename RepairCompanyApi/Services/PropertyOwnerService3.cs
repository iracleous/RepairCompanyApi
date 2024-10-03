using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class PropertyOwnerService3: IPropertyOwnerService
{
    private readonly IRepository<PropertyOwner> _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerService3> _logger;

    public PropertyOwnerService3(IRepository<PropertyOwner> propertyOwnerRepository, ILogger<PropertyOwnerService3> logger)
    {
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
    }

    public Task<IActionResult> AssignPropertyToOwner(long popertyOwnerId, long propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeletePropertyOwner(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerData(int pageCount, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<PropertyOwner>> GetPropertyOwner(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner)
    {
        throw new NotImplementedException();
    }
}
