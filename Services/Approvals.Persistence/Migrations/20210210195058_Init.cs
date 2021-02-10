using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Approvals.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Approvals",
                table => new
                {
                    Id = table.Column<string>("text", nullable: false),
                    Balance = table.Column<float>("real", nullable: false),
                    Approved = table.Column<bool>("boolean", nullable: false),
                    Pending = table.Column<bool>("boolean", nullable: false),
                    Blocked = table.Column<bool>("boolean", nullable: false),
                    LastSequentialNumber = table.Column<int>("integer", nullable: false),
                    EntityId = table.Column<string>("text", nullable: true),
                    ProfileId = table.Column<string>("text", nullable: true),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Version = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Approvals", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Approvals");
        }
    }
}