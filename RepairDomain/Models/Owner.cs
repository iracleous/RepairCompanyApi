namespace RepairCompanyApi.Models;

public class Owner
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public virtual List<Address> Addresses { get; set; } = [];
}


public class Address
{
    public long Id { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public virtual List<Owner> Owners { get; set; } = [];
}
