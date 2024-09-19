using Microsoft.EntityFrameworkCore;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Data
{
    public class RepairDbContext:DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=(local);Initial Catalog=repairDb-2024;User Id=sa; Password=admin!@#123;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
