using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Services;

public interface IPropertyOwnerService
{

    Task<ApiResult<long>> CreatePropertyOwner(PropertyOwnerDtoRequest propertyOwnerDto);
    Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerDataAsync(int pageCount, int pageSize);
    Task<ActionResult<Statistics>> CalculateStatistics();



    Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize);
    Task<ActionResult<OwnerDataDto?>> GetPropertyOwner(long id);
    Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner);
    // Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner);


    Task<IActionResult> DeletePropertyOwner(long id);

    Task<IActionResult> AssignPropertyToOwner(long popertyOwnerId, long propertyId);

   
 }
