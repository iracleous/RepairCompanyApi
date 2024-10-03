using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class PropertyOwnerService2: IPropertyOwnerService
{
    private readonly IMapper _mapper;
    private readonly IPropertyOwnerRepository _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerService2> _logger;

    public PropertyOwnerService2(IMapper mapper, 
        IPropertyOwnerRepository propertyOwnerRepository,
        ILogger<PropertyOwnerService2> logger)
    {
        _mapper = mapper;
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
    }

    public async Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerData(int pageCount, int pageSize)
    {
        _logger.LogDebug("start");
        if (pageCount <= 0) pageCount = 1;
        if (pageSize <= 0 || pageSize > 20) pageSize = 10;

        IEnumerable<PropertyOwner> propertyOwners =
            await _propertyOwnerRepository
            .GetAllAsync(pageCount, pageSize);

        IEnumerable<OwnerDataDto> destinationList =
            _mapper.Map<List<OwnerDataDto>>(propertyOwners);

        return destinationList.ToList();
    }


    public Task<IActionResult> AssignPropertyToOwner(long popertyOwnerId, long propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeletePropertyOwner(long id)
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

