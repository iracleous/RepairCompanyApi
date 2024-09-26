using System;
using System.Collections.Generic;

namespace RepairCompanyApi.Models2;

public partial class Repair
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public int RepairStatus { get; set; }

    public long? BuildingPropertyId { get; set; }

    public virtual BuildingProperty? BuildingProperty { get; set; }
}
