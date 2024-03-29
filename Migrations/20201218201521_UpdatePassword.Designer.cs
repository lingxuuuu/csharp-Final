﻿// <auto-generated />
using System;
using BeltExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeltExam.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20201218201521_UpdatePassword")]
    partial class UpdatePassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BeltExam.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Duration")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Time")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("BeltExam.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("BeltExam.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeltExam.Models.Activity", b =>
                {
                    b.HasOne("BeltExam.Models.User", "PlanedBy")
                        .WithMany("PlanedActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeltExam.Models.Participant", b =>
                {
                    b.HasOne("BeltExam.Models.Activity", "Activity")
                        .WithMany("Participants")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeltExam.Models.User", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
