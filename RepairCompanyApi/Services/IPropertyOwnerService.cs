using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services;

public interface IPropertyOwnerService
{
    public Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize);
    public Task<ActionResult<PropertyOwner>> GetPropertyOwner(long id);
    public Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner);
    public Task<ActionResult<PropertyOwner>> PostPropertyOwner
        (PropertyOwner propertyOwner);
    public Task<IActionResult> DeletePropertyOwner(long id);

    public Task<IActionResult> AssignPropertyToOwner(long popertyOwnerId, long propertyId);

    public Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerData(int pageCount, int pageSize);
 }
