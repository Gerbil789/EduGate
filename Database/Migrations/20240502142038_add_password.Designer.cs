﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(EduGateDbContext))]
    [Migration("20240502142038_add_password")]
    partial class add_password
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("Database.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Database.Models.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StudyProgramId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ApplicationId");

                    b.HasIndex("StudyProgramId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Database.Models.Phone", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PhoneId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Database.Models.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SchoolId");

                    b.HasIndex("AddressId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Database.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Application1ApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Application2ApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Application3ApplicationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId");

                    b.HasIndex("AddressId");

                    b.HasIndex("Application1ApplicationId");

                    b.HasIndex("Application2ApplicationId");

                    b.HasIndex("Application3ApplicationId");

                    b.HasIndex("PhoneId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Database.Models.StudyProgram", b =>
                {
                    b.Property<int>("StudyProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudyProgramId");

                    b.HasIndex("SchoolId");

                    b.ToTable("StudyPrograms");
                });

            modelBuilder.Entity("Database.Models.Application", b =>
                {
                    b.HasOne("Database.Models.StudyProgram", "StudyProgram")
                        .WithMany()
                        .HasForeignKey("StudyProgramId");

                    b.Navigation("StudyProgram");
                });

            modelBuilder.Entity("Database.Models.School", b =>
                {
                    b.HasOne("Database.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Database.Models.Student", b =>
                {
                    b.HasOne("Database.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Application", "Application1")
                        .WithMany()
                        .HasForeignKey("Application1ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Application", "Application2")
                        .WithMany()
                        .HasForeignKey("Application2ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Application", "Application3")
                        .WithMany()
                        .HasForeignKey("Application3ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Application1");

                    b.Navigation("Application2");

                    b.Navigation("Application3");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("Database.Models.StudyProgram", b =>
                {
                    b.HasOne("Database.Models.School", null)
                        .WithMany("StudyPrograms")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("Database.Models.School", b =>
                {
                    b.Navigation("StudyPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
