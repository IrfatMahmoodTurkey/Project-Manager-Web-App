﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementWebApp.Models.Context;

namespace ProjectManagementWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190511043412_CreateProjectTable")]
    partial class CreateProjectTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectManagementWebApp.Models.Designation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DesignationName")
                        .IsRequired();

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("EndDate")
                        .IsRequired();

                    b.Property<string>("StartDate")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DesignationId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DesignationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.User", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.Designation", "Designation")
                        .WithMany("Users")
                        .HasForeignKey("DesignationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}