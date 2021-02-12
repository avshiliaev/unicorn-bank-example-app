﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Approvals.Persistence.Migrations
{
    [DbContext(typeof(ApprovalsContext))]
    internal class ApprovalsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Approvals.Persistence.Models.AccountRecord", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<bool>("Approved")
                    .HasColumnType("boolean");

                b.Property<float>("Balance")
                    .HasColumnType("real");

                b.Property<bool>("Blocked")
                    .HasColumnType("boolean");

                b.Property<DateTime>("Created")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("EntityId")
                    .HasColumnType("text");

                b.Property<int>("LastSequentialNumber")
                    .HasColumnType("integer");

                b.Property<bool>("Pending")
                    .HasColumnType("boolean");

                b.Property<string>("ProfileId")
                    .HasColumnType("text");

                b.Property<DateTime>("Updated")
                    .HasColumnType("timestamp without time zone");

                b.Property<int>("Version")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.ToTable("Approvals");
            });
#pragma warning restore 612, 618
        }
    }
}