using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Data;

public class RepairDbContext:DbContext
{
    private readonly IConfiguration _configuration;

    public RepairDbContext(DbContextOptions<RepairDbContext> options, 
        IConfiguration configuration)
    : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    public DbSet<PropertyOwner> PropertyOwners { get; set; }
    public DbSet<BuildingProperty> BuildingProperties { get; set; }
    public DbSet<Repair> Repairs { get; set; }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

}
