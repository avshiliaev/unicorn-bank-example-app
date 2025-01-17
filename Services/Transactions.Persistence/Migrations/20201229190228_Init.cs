﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transactions.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Accounts",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    Balance = table.Column<float>("real", nullable: false),
                    ProfileId = table.Column<string>("text", nullable: true),
                    Approved = table.Column<bool>("boolean", nullable: false),
                    Pending = table.Column<bool>("boolean", nullable: false),
                    Blocked = table.Column<bool>("boolean", nullable: false),
                    LastSequentialNumber = table.Column<int>("integer", nullable: false),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Version = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Accounts", x => x.Id); });

            migrationBuilder.CreateTable(
                "Transactions",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    AccountId = table.Column<Guid>("uuid", nullable: false),
                    ProfileId = table.Column<string>("text", nullable: true),
                    Amount = table.Column<float>("real", nullable: false),
                    Info = table.Column<string>("text", nullable: true),
                    Approved = table.Column<bool>("boolean", nullable: false),
                    Pending = table.Column<bool>("boolean", nullable: false),
                    Blocked = table.Column<bool>("boolean", nullable: false),
                    SequentialNumber = table.Column<int>("integer", nullable: false),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Version = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Transactions", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Accounts");

            migrationBuilder.DropTable(
                "Transactions");
        }
    }
}