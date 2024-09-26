namespace RepairCompanyApi.Dtos;

public class OwnerData
{
    public long OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public List<BuildingOwnerDto> Buildings { get; set; } = [];
}

public class BuildingOwnerDto
{
    public long BuildingId { get; set; }
    public string? Address { get; set; }
}