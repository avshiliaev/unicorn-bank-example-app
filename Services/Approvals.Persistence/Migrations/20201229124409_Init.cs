using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Approvals.Persistence.Migrations
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
                    AccountId = table.Column<Guid>("uuid", nullable: false),
                    Approved = table.Column<bool>("boolean", nullable: false),
                    Pending = table.Column<bool>("boolean", nullable: false),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Version = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Accounts", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Accounts");
        }
    }
}