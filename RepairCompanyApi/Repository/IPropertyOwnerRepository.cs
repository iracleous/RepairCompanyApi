using Microsoft.AspNetCore.Mvc;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Repository;

public interface IPropertyOwnerRepository
{
    Task<IEnumerable<PropertyOwner>> GetAllAsync(int pageCount, int pageSize);
    Task<PropertyOwner?> GetByIdAsync(long id);
    Task<bool> UpdateAsync(PropertyOwner propertyOwner);
    Task<long> AddAsync(PropertyOwner propertyOwner);
    Task<bool> DeleteAsync(long id);
    Task<PropertyOwner?> GetByNameAsync(string name);
}
