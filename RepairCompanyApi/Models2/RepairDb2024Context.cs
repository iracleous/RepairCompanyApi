using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepairCompanyApi.Models2;

public partial class RepairDb2024Context : DbContext
{
    public RepairDb2024Context()
    {
    }

    public RepairDb2024Context(DbContextOptions<RepairDb2024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BuildingProperty> BuildingProperties { get; set; }

    public virtual DbSet<PropertyOwner> PropertyOwners { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=repairDb-2024;User Id=sa; Password=admin!@#123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuildingProperty>(entity =>
        {
            entity.HasIndex(e => e.PropertyOwnerId, "IX_BuildingProperties_PropertyOwnerId");

            entity.HasOne(d => d.PropertyOwner).WithMany(p => p.BuildingProperties).HasForeignKey(d => d.PropertyOwnerId);
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasIndex(e => e.BuildingPropertyId, "IX_Repairs_BuildingPropertyId");

            entity.HasOne(d => d.BuildingProperty).WithMany(p => p.Repairs).HasForeignKey(d => d.BuildingPropertyId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
