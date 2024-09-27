using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class PropertyOwnerService2
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

    public async Task<ActionResult<IEnumerable<OwnerData>>> GetOwnerData(int pageCount, int pageSize)
    {
        _logger.LogDebug("start");
        if (pageCount <= 0) pageCount = 1;
        if (pageSize <= 0 || pageSize > 20) pageSize = 10;

        IEnumerable<PropertyOwner> propertyOwners =
            await _propertyOwnerRepository
            .GetAllAsync(pageCount, pageSize);

        IEnumerable<OwnerData> destinationList = 
            _mapper.Map<List<OwnerData>>(propertyOwners);

        return    destinationList.ToList();
    }
}

