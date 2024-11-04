﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopTARge23.Data;

#nullable disable

namespace ShopTARge23.Data.Migrations
{
    [DbContext(typeof(ShopTARge23Context))]
    partial class ShopTARge23ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShopTARge23.Core.Domain.FileToApi", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("ExistingFilePath")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid?>("SpaceshipId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.ToTable("FileToApis");
            });

            modelBuilder.Entity("ShopTARge23.Core.Domain.FileToDatabase", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<byte[]>("ImageData")
                    .HasColumnType("varbinary(max)");

                b.Property<string>("ImageTitle")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid?>("RealEstateId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.ToTable("FileToDatabases");
            });

            modelBuilder.Entity("ShopTARge23.Core.Domain.FileToData", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("ExistingFilePath")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<byte[]>("ImageDatas")
                    .HasColumnType("varbinary(max)");

                b.Property<string>("ImageTitles")
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid?>("KindergartenId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("Id");

                b.ToTable("FileToDatas");
            });

            modelBuilder.Entity("ShopTARge23.Core.Domain.RealEstate", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Location")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<int?>("ChildrenCount")
                    .HasColumnType("int");

                b.Property<string>("BuildingType")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("datetime2");

                b.Property<int?>("RoomNumber")
                    .HasColumnType("int");

                b.Property<double?>("Size")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.ToTable("RealEstates");
            });

            modelBuilder.Entity("ShopTARge23.Core.Domain.Kindergarten", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("BuildingType")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<int>("ChildrenCount")
                    .HasColumnType("int");

                b.Property<string>("Location")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("datetime2");

                b.Property<int?>("RoomNumber")
                    .HasColumnType("int");

                b.Property<double?>("Size")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.ToTable("Kindergartens");
            });

            modelBuilder.Entity("ShopTARge23.Core.Domain.Spaceship", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("BuiltDate")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<int>("Crew")
                    .HasColumnType("int");

                b.Property<int>("EnginePower")
                    .HasColumnType("int");

                b.Property<DateTime>("ModifiedAt")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("SpaceshipModel")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Typename")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Spaceships");
            });
#pragma warning restore 612, 618
        }
    }
}
