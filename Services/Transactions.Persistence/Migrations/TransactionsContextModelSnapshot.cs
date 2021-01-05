﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Transactions.Persistence.Migrations
{
    [DbContext(typeof(TransactionsContext))]
    internal class TransactionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Transactions.Persistence.Entities.AccountEntity", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<bool>("Approved")
                    .HasColumnType("boolean");

                b.Property<float>("Balance")
                    .HasColumnType("real");

                b.Property<bool>("Blocked")
                    .HasColumnType("boolean");

                b.Property<DateTime>("Created")
                    .HasColumnType("timestamp without time zone");

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

                b.ToTable("Accounts");
            });

            modelBuilder.Entity("Transactions.Persistence.Entities.TransactionEntity", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<Guid>("AccountId")
                    .HasColumnType("uuid");

                b.Property<float>("Amount")
                    .HasColumnType("real");

                b.Property<bool>("Approved")
                    .HasColumnType("boolean");

                b.Property<bool>("Blocked")
                    .HasColumnType("boolean");

                b.Property<DateTime>("Created")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Info")
                    .HasColumnType("text");

                b.Property<bool>("Pending")
                    .HasColumnType("boolean");

                b.Property<string>("ProfileId")
                    .HasColumnType("text");

                b.Property<int>("SequentialNumber")
                    .HasColumnType("integer");

                b.Property<DateTime>("Updated")
                    .HasColumnType("timestamp without time zone");

                b.Property<int>("Version")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.ToTable("Transactions");
            });
#pragma warning restore 612, 618
        }
    }
}