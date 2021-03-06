﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementWebApp.Models.Context;

namespace ProjectManagementWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectManagementWebApp.Models.AssignProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId");

                    b.Property<int>("State");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignProjects");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentDescription")
                        .IsRequired();

                    b.Property<string>("Date");

                    b.Property<string>("Mension")
                        .IsRequired();

                    b.Property<int>("ProjectId");

                    b.Property<int>("Seen");

                    b.Property<int>("State");

                    b.Property<int>("TaskId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

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

            modelBuilder.Entity("ProjectManagementWebApp.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileExtension");

                    b.Property<string>("FileName");

                    b.Property<string>("FileNameWithExtension");

                    b.Property<int>("ProjectId");

                    b.Property<int>("State");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Description");

                    b.Property<string>("EndDate")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("StartDate")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ByUserId");

                    b.Property<string>("Description");

                    b.Property<string>("DueDate")
                        .IsRequired();

                    b.Property<string>("Priority")
                        .IsRequired();

                    b.Property<int>("ProjectId");

                    b.Property<int>("Seen");

                    b.Property<int>("State");

                    b.Property<int>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("ByUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Tasks");
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

                    b.Property<string>("ProfileUrl");

                    b.Property<int>("State");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DesignationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.UserAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PageId");

                    b.Property<int>("State");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccesses");
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.AssignProject", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.Project", "Project")
                        .WithMany("AssignProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementWebApp.Models.User", "User")
                        .WithMany("AssignProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Comment", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementWebApp.Models.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementWebApp.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.File", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.Project", "Project")
                        .WithMany("Files")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.Task", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.User", "ByUser")
                        .WithMany("TasksBy")
                        .HasForeignKey("ByUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementWebApp.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagementWebApp.Models.User", "ToUser")
                        .WithMany("TasksTo")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.User", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.Designation", "Designation")
                        .WithMany("Users")
                        .HasForeignKey("DesignationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagementWebApp.Models.UserAccess", b =>
                {
                    b.HasOne("ProjectManagementWebApp.Models.User", "User")
                        .WithMany("UserAccesses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
