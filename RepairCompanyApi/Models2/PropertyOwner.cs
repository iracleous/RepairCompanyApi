using System;
using System.Collections.Generic;

namespace RepairCompanyApi.Models2;

public partial class PropertyOwner
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<BuildingProperty> BuildingProperties { get; set; } = new List<BuildingProperty>();
}
