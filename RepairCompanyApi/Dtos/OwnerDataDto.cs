namespace RepairCompanyApi.Dtos;

public class OwnerDataDto
{
    public long OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public List<BuildingOwnerDto> Buildings { get; set; } = [];
}

public class BuildingOwnerDto
{
    public long Id { get; set; }
    public string? Address { get; set; }
    public List<BuildingRepairDto> Repairs { get; set; } = [];
}

public class BuildingRepairDto
{
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
}