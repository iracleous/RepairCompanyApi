using System.Text.Json.Serialization;

namespace RepairCompanyApi.Models;

public class PropertyOwner
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    
    virtual public List<BuildingProperty> BuildingProperties { get; set; } 
        = new List<BuildingProperty>();
}

public class BuildingProperty
{
    public long Id { get; set; }
    public string Address { get; set; } = string.Empty;
    virtual public PropertyOwner? PropertyOwner { get; set; }
    virtual public List<Repair> Repairs { get; set; }
            = new List<Repair>();
}

public class Repair
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationDate   { get; set; }
    public RepairStatus RepairStatus { get; set; } = RepairStatus.PENDING;
    virtual public BuildingProperty? BuildingProperty { get; set; }
}

public enum RepairStatus
{
    PENDING, DONE, CANCELLED
}