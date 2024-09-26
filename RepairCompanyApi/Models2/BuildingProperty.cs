using System;
using System.Collections.Generic;

namespace RepairCompanyApi.Models2;

public partial class BuildingProperty
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public long? PropertyOwnerId { get; set; }

    public virtual PropertyOwner? PropertyOwner { get; set; }

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
