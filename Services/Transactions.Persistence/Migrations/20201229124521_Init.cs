﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transactions.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: true),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    Pending = table.Column<bool>(type: "boolean", nullable: false),
                    SequentialNumber = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
