﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaCepConsumer.Api.Infrastructure.Contexts;

#nullable disable

namespace ViaCepConsumer.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240310211820_DatabaseCreation")]
    partial class DatabaseCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ViaCepConsumer.Api.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("PK_Roles_Id");

                    b.HasIndex(new[] { "Id" }, "IX_Roles_Id")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "IX_Roles_Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("ViaCepConsumer.Api.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Email");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR(255)")
                        .HasColumnName("Password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Username");

                    b.HasKey("Id")
                        .HasName("PK_Users_Id");

                    b.HasIndex(new[] { "Email" }, "IX_Users_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Id" }, "IX_Users_Id")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "IX_Users_Username")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("UserRoles", b =>
                {
                    b.HasOne("ViaCepConsumer.Api.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_RoleId");

                    b.HasOne("ViaCepConsumer.Api.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
