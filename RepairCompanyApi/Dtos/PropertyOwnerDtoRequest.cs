using RepairCompanyApi.Models;

namespace RepairCompanyApi.Dtos;

public class PropertyOwnerDtoRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
