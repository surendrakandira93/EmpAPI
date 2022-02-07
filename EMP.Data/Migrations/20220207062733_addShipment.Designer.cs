﻿// <auto-generated />
using System;
using EMP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMP.Data.Migrations
{
    [DbContext(typeof(EmpContext))]
    [Migration("20220207062733_addShipment")]
    partial class addShipment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("EMP.Data.EmpGroupList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<int>("InviteType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("EmpGroupList");
                });

            modelBuilder.Entity("EMP.Data.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasMaxLength(256)
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateOfBrith")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("ImageURL");

                    b.Property<string>("LinkedinURL")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("LinkedinURL");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("EMP.Data.EmployeeGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(550)
                        .HasColumnType("TEXT");

                    b.Property<string>("IconImg")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmployeeGroup");
                });

            modelBuilder.Entity("EMP.Data.EmployeeTechnology", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmpId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.ToTable("EmployeeTechnology");
                });

            modelBuilder.Entity("EMP.Data.Shipment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("APIKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("Broker")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsLive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password2")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Platform")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("EMP.Data.EmpGroupList", b =>
                {
                    b.HasOne("EMP.Data.EmployeeGroup", "Group")
                        .WithMany("Employee")
                        .HasForeignKey("GroupId")
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("EMP.Data.EmployeeTechnology", b =>
                {
                    b.HasOne("EMP.Data.Employee", "Emp")
                        .WithMany("EmployeeTechnologies")
                        .HasForeignKey("EmpId")
                        .IsRequired();

                    b.Navigation("Emp");
                });

            modelBuilder.Entity("EMP.Data.Employee", b =>
                {
                    b.Navigation("EmployeeTechnologies");
                });

            modelBuilder.Entity("EMP.Data.EmployeeGroup", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
