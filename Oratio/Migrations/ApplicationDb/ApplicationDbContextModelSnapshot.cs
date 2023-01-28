﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oratio.Data;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Oratio.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChurchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChurchId")
                        .IsUnique()
                        .HasFilter("[ChurchId] IS NOT NULL");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Oratio.Models.Church", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("Churches");
                });

            modelBuilder.Entity("Oratio.Models.Intention", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AskedIntention")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Offering")
                        .HasColumnType("real");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MassId");

                    b.ToTable("Intentions");
                });

            modelBuilder.Entity("Oratio.Models.Mass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChurchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChurchId");

                    b.ToTable("Mass");
                });

            modelBuilder.Entity("Oratio.Models.MassGenerationRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RuleStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RuleTerminationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParishId");

                    b.ToTable("MassGenerationRules");
                });

            modelBuilder.Entity("Oratio.Models.Parish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Dedicated")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("MinimumOffering")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Parishes");
                });

            modelBuilder.Entity("Oratio.Models.Address", b =>
                {
                    b.HasOne("Oratio.Models.Church", "Church")
                        .WithOne("Address")
                        .HasForeignKey("Oratio.Models.Address", "ChurchId");

                    b.Navigation("Church");
                });

            modelBuilder.Entity("Oratio.Models.Church", b =>
                {
                    b.HasOne("Oratio.Models.Parish", "Parish")
                        .WithMany("Churches")
                        .HasForeignKey("ParishId");

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Oratio.Models.Intention", b =>
                {
                    b.HasOne("Oratio.Models.Mass", "Mass")
                        .WithMany("Intentions")
                        .HasForeignKey("MassId");

                    b.Navigation("Mass");
                });

            modelBuilder.Entity("Oratio.Models.Mass", b =>
                {
                    b.HasOne("Oratio.Models.Church", "Church")
                        .WithMany("Masses")
                        .HasForeignKey("ChurchId");

                    b.Navigation("Church");
                });

            modelBuilder.Entity("Oratio.Models.MassGenerationRule", b =>
                {
                    b.HasOne("Oratio.Models.Parish", null)
                        .WithMany("MassGenerationRules")
                        .HasForeignKey("ParishId");
                });

            modelBuilder.Entity("Oratio.Models.Church", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Masses");
                });

            modelBuilder.Entity("Oratio.Models.Mass", b =>
                {
                    b.Navigation("Intentions");
                });

            modelBuilder.Entity("Oratio.Models.Parish", b =>
                {
                    b.Navigation("Churches");

                    b.Navigation("MassGenerationRules");
                });
#pragma warning restore 612, 618
        }
    }
}
