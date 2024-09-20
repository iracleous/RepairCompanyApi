using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services;

public interface IPropertyOwnerService
{
    public   Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners();
    public   Task<ActionResult<PropertyOwner>> GetPropertyOwner(long id);
    public   Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner);
    public  Task<ActionResult<PropertyOwner>> PostPropertyOwner
        (PropertyOwner propertyOwner);
    public Task<IActionResult> DeletePropertyOwner(long id);
 }
