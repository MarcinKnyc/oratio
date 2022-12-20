﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oratio.Data;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221205115416_Add-Owners")]
    partial class AddOwners
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Oratio.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChurchId")
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
                        .IsUnique();

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

                    b.Property<Guid>("ParishId")
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

                    b.Property<int?>("TimesToRepeat")
                        .HasColumnType("int");

                    b.Property<string>("TimespanToRepeat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WeekNumber")
                        .HasColumnType("int");

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

                    b.HasData(
                        new
                        {
                            Id = new Guid("264ac181-b7dd-4cd6-8e30-51a06c496059"),
                            Dedicated = "Żadna",
                            Name = "Fejk"
                        });
                });

            modelBuilder.Entity("Oratio.Models.Address", b =>
                {
                    b.HasOne("Oratio.Models.Church", "Church")
                        .WithOne("Address")
                        .HasForeignKey("Oratio.Models.Address", "ChurchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Church");
                });

            modelBuilder.Entity("Oratio.Models.Church", b =>
                {
                    b.HasOne("Oratio.Models.Parish", "Parish")
                        .WithMany("Churches")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Oratio.Models.Intention", b =>
                {
                    b.HasOne("Oratio.Models.Mass", "Mass")
                        .WithMany()
                        .HasForeignKey("MassId");

                    b.Navigation("Mass");
                });

            modelBuilder.Entity("Oratio.Models.Mass", b =>
                {
                    b.HasOne("Oratio.Models.Church", "Church")
                        .WithMany()
                        .HasForeignKey("ChurchId");

                    b.Navigation("Church");
                });

            modelBuilder.Entity("Oratio.Models.MassGenerationRule", b =>
                {
                    b.HasOne("Oratio.Models.Parish", "Parish")
                        .WithMany()
                        .HasForeignKey("ParishId");

                    b.Navigation("Parish");
                });

            modelBuilder.Entity("Oratio.Models.Church", b =>
                {
                    b.Navigation("Address");
                });

            modelBuilder.Entity("Oratio.Models.Parish", b =>
                {
                    b.Navigation("Churches");
                });
#pragma warning restore 612, 618
        }
    }
}