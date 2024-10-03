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

namespace RepairCompanyApi.Services;

public class PropertyOwnerService2: IPropertyOwnerService
{
    private readonly IMapper _mapper;
    private readonly IPropertyOwnerRepository _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerService2> _logger;
    private readonly IDistributedCache _cache;

    public PropertyOwnerService2(IMapper mapper, IPropertyOwnerRepository propertyOwnerRepository, 
        ILogger<PropertyOwnerService2> logger, IDistributedCache cache)
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

  

    public Task<ActionResult<PropertyOwner>> GetPropertyOwner(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<PropertyOwner>>> GetPropertyOwners(int pageCount, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<PropertyOwner>> PostPropertyOwner(PropertyOwner propertyOwner)
    {
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
}

