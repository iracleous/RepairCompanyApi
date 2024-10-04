using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;
using StackExchange.Redis;
using System.Drawing.Printing;

namespace RepairCompanyApi.Services.Implementations;

public class PropertyOwnerServiceUsingRepository : IPropertyOwnerService
{
    private readonly IMapper _mapper;
    private readonly IPropertyOwnerRepository _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerServiceUsingRepository> _logger;
    private readonly IDistributedCache _cache;

    public PropertyOwnerServiceUsingRepository(IMapper mapper, IPropertyOwnerRepository propertyOwnerRepository,
        ILogger<PropertyOwnerServiceUsingRepository> logger, IDistributedCache cache)
    {
        _mapper = mapper;
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<ActionResult<IEnumerable<OwnerDataDto>>> GetOwnerDataAsync(int pageCount, int pageSize)
    {
        _logger.LogDebug("start");
        string cacheKey = $"pageCount:{pageCount}:pageSize:{pageSize}";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cachedData))
        {

            if (pageCount <= 0) pageCount = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 10;

            IEnumerable<PropertyOwner> propertyOwners =
                await _propertyOwnerRepository
                .GetAllAsync(pageCount, pageSize);

            IEnumerable<OwnerDataDto> destinationList =
                _mapper.Map<List<OwnerDataDto>>(propertyOwners);

            var result = destinationList.ToList();
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            return result;
        }
        IEnumerable<OwnerDataDto>? result1 = JsonConvert.DeserializeObject<IEnumerable<OwnerDataDto>>(cachedData);
        Console.WriteLine("cache was used");
        return result1 != null ? result1.ToList() : [];
    }


    public Task<IActionResult> AssignPropertyToOwner(long popertyOwnerId, long propertyId)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeletePropertyOwner(long id)
    {
        throw new NotImplementedException();
    }



    public async Task<ActionResult<OwnerDataDto?>> GetPropertyOwner(long id)
    {
        _logger.LogDebug("start");
        string cacheKey = $"ownerId:{id}";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cachedData))
        {
            PropertyOwner? propertyOwner =
                await _propertyOwnerRepository
                .GetByIdAsync(id);

            OwnerDataDto ownerDataDto = _mapper.Map<OwnerDataDto>(propertyOwner);

            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(ownerDataDto), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            return ownerDataDto;
        }
        OwnerDataDto? ownerDataDtoFromDb = 
            JsonConvert.DeserializeObject<OwnerDataDto>(cachedData);
        Console.WriteLine("cache was used");
        return ownerDataDtoFromDb ;

    }

    public Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner)
    {
        if (propertyOwner == null) 
            return new BadRequestResult(); 
        if (propertyOwner.Address == null || !propertyOwner.Address.Equals("Athens"))
            throw new ArgumentNullException(nameof(propertyOwner));

        bool returnValue = await _propertyOwnerRepository.AddAsync(propertyOwner);
        await cleanKeysAsync();
        return propertyOwner;
    }

    public Task<IActionResult> PutPropertyOwner(long id, PropertyOwner propertyOwner)
    {
        throw new NotImplementedException();
    }

    private async Task cleanKeysAsync()
    {
        int pageCount = 1;
        int pageSize = 2;
        var key = $"pageCount:{pageCount}:pageSize:{pageSize}";
        await _cache.RemoveAsync(key);
    }

    public Task<ActionResult<Statistics>> CalculateStatistics()
    {
        throw new NotImplementedException();
    }
}

