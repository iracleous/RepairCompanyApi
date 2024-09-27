using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services;

public class GenService3<T> where T : class
{
    private readonly IRepository<T> _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerService3> _logger;

    public GenService3(IRepository<T> propertyOwnerRepository, ILogger<PropertyOwnerService3> logger)
    {
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
    }

}
