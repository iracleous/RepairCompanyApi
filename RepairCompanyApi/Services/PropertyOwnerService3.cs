using RepairCompanyApi.Models;
using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class PropertyOwnerService3
{
    private readonly IRepository<PropertyOwner> _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerService3> _logger;

    public PropertyOwnerService3(IRepository<PropertyOwner> propertyOwnerRepository, ILogger<PropertyOwnerService3> logger)
    {
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
    }


}
