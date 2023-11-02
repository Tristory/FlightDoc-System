﻿// <auto-generated />
using System;
using FlightDocs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightDocs.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231031052832_SecondMigration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FlightDocs.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GroupId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("FlightDocs.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("File_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature_Filepath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("FlightId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("FlightDocs.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("Customer_roleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Customer_roleId");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("FlightDocs.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AircratNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Arrival_Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Departure_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("From_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightDocs.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("FlightDocs.Models.OldVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<string>("File_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature_Filepath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("OldVersions");
                });

            modelBuilder.Entity("FlightDocs.Models.PermissionDG", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("GroupId");

                    b.ToTable("PermissionDG");
                });

            modelBuilder.Entity("FlightDocs.Models.PermissionDTG", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Access_Level")
                        .HasColumnType("int");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("GroupId");

                    b.ToTable("PermissionDTG");
                });

            modelBuilder.Entity("FlightDocs.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FlightDocs.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature_Filepath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightDocs.Models.Account", b =>
                {
                    b.HasOne("FlightDocs.Models.Group", "Group")
                        .WithMany("Accounts")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("FlightDocs.Models.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightDocs.Models.Document", b =>
                {
                    b.HasOne("FlightDocs.Models.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.Flight", "Flight")
                        .WithMany("Documents")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlightDocs.Models.DocumentType", b =>
                {
                    b.HasOne("FlightDocs.Models.User", "User")
                        .WithMany("DocumentTypes")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.Role", "Role")
                        .WithMany("DocumentTypes")
                        .HasForeignKey("Customer_roleId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightDocs.Models.Group", b =>
                {
                    b.HasOne("FlightDocs.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightDocs.Models.OldVersion", b =>
                {
                    b.HasOne("FlightDocs.Models.Document", "Document")
                        .WithMany("OldVersions")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("FlightDocs.Models.PermissionDG", b =>
                {
                    b.HasOne("FlightDocs.Models.Document", "Document")
                        .WithMany("PermissionDGs")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.Group", "Group")
                        .WithMany("PermissionDGs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("FlightDocs.Models.PermissionDTG", b =>
                {
                    b.HasOne("FlightDocs.Models.DocumentType", "DocumentType")
                        .WithMany("PermissionDTGs")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightDocs.Models.Group", "Group")
                        .WithMany("PermissionDTGs")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("FlightDocs.Models.Document", b =>
                {
                    b.Navigation("OldVersions");

                    b.Navigation("PermissionDGs");
                });

            modelBuilder.Entity("FlightDocs.Models.DocumentType", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("PermissionDTGs");
                });

            modelBuilder.Entity("FlightDocs.Models.Flight", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("FlightDocs.Models.Group", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("PermissionDGs");

                    b.Navigation("PermissionDTGs");
                });

            modelBuilder.Entity("FlightDocs.Models.Role", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("DocumentTypes");
                });

            modelBuilder.Entity("FlightDocs.Models.User", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();

                    b.Navigation("DocumentTypes");

                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
