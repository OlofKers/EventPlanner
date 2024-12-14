﻿// <auto-generated />
using System;
using EventPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventPlanner.Migrations
{
    [DbContext(typeof(EventplannerContext))]
    [Migration("20241214113135_AddFKRolesForReal")]
    partial class AddFKRolesForReal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventPlanner.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EventPlanner.Models.Gathering", b =>
                {
                    b.Property<int>("GatheringId")
                        .HasColumnType("int");

                    b.Property<string>("GatheringDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GatheringEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("GatheringName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("GatheringStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("MinorsAllowed")
                        .HasColumnType("bit");

                    b.HasKey("GatheringId");

                    b.ToTable("Gatherings");
                });

            modelBuilder.Entity("EventPlanner.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .HasColumnType("int");

                    b.HasKey("RegistrationId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("EventPlanner.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("EventPlanner.Models.Suggestion", b =>
                {
                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.Property<string>("SuggestionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SuggestionsDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SuggestionId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("EventPlanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserAge")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1,
                            UserAge = 18,
                            UserName = "DefaultUser",
                            UserPassword = "123456"
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2,
                            UserAge = 17,
                            UserName = "DefaultAdmin",
                            UserPassword = "password123"
                        });
                });

            modelBuilder.Entity("EventPlanner.Models.Gathering", b =>
                {
                    b.HasOne("EventPlanner.Models.Category", "GatheringCategory")
                        .WithMany("Gatherings")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GatheringCategory");
                });

            modelBuilder.Entity("EventPlanner.Models.Registration", b =>
                {
                    b.HasOne("EventPlanner.Models.Gathering", "RegistrationGathering")
                        .WithMany("Registrations")
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventPlanner.Models.User", "RegistrationOwner")
                        .WithMany("Registrations")
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegistrationGathering");

                    b.Navigation("RegistrationOwner");
                });

            modelBuilder.Entity("EventPlanner.Models.Suggestion", b =>
                {
                    b.HasOne("EventPlanner.Models.User", "SuggestionOwner")
                        .WithMany("Suggestions")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SuggestionOwner");
                });

            modelBuilder.Entity("EventPlanner.Models.User", b =>
                {
                    b.HasOne("EventPlanner.Models.Role", "Role")
                        .WithMany("UsersWithRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EventPlanner.Models.Category", b =>
                {
                    b.Navigation("Gatherings");
                });

            modelBuilder.Entity("EventPlanner.Models.Gathering", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("EventPlanner.Models.Role", b =>
                {
                    b.Navigation("UsersWithRole");
                });

            modelBuilder.Entity("EventPlanner.Models.User", b =>
                {
                    b.Navigation("Registrations");

                    b.Navigation("Suggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
