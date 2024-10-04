﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepairCompanyApi.Data;

#nullable disable

namespace RepairCompanyApi.Migrations
{
    [DbContext(typeof(RepairDbContext))]
    partial class RepairDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AddressOwner", b =>
                {
                    b.Property<long>("AddressesId")
                        .HasColumnType("bigint");

                    b.Property<long>("OwnersId")
                        .HasColumnType("bigint");

                    b.HasKey("AddressesId", "OwnersId");

                    b.HasIndex("OwnersId");

                    b.ToTable("AddressOwner");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.BuildingProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PropertyOwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PropertyOwnerId");

                    b.ToTable("BuildingProperties");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.Owner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.PropertyOwner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PropertyOwners");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.Repair", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("BuildingPropertyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RepairStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingPropertyId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.WeatherForecast", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("AddressOwner", b =>
                {
                    b.HasOne("RepairCompanyApi.Models.Address", null)
                        .WithMany()
                        .HasForeignKey("AddressesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepairCompanyApi.Models.Owner", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepairCompanyApi.Models.BuildingProperty", b =>
                {
                    b.HasOne("RepairCompanyApi.Models.PropertyOwner", "PropertyOwner")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("PropertyOwnerId");

                    b.Navigation("PropertyOwner");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.Repair", b =>
                {
                    b.HasOne("RepairCompanyApi.Models.BuildingProperty", "BuildingProperty")
                        .WithMany("Repairs")
                        .HasForeignKey("BuildingPropertyId");

                    b.Navigation("BuildingProperty");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.BuildingProperty", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("RepairCompanyApi.Models.PropertyOwner", b =>
                {
                    b.Navigation("BuildingProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
