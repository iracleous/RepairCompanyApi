using RepairCompanyApi.Repository;

namespace RepairCompanyApi.Services.Implementations;

public class GenService3<T> where T : class
{
    private readonly IRepository<T> _propertyOwnerRepository;
    private readonly ILogger<PropertyOwnerServiceUsingRepository> _logger;

    public GenService3(IRepository<T> propertyOwnerRepository, ILogger<PropertyOwnerServiceUsingRepository> logger)
    {
        _propertyOwnerRepository = propertyOwnerRepository;
        _logger = logger;
    }
}
