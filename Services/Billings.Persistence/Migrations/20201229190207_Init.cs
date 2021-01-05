using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billings.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Billings",
                table => new
                {
                    Id = table.Column<Guid>("uuid", nullable: false),
                    ProfileId = table.Column<string>("text", nullable: true),
                    AccountId = table.Column<Guid>("uuid", nullable: false),
                    TransactionId = table.Column<Guid>("uuid", nullable: false),
                    Amount = table.Column<float>("real", nullable: false),
                    Approved = table.Column<bool>("boolean", nullable: false),
                    Pending = table.Column<bool>("boolean", nullable: false),
                    Blocked = table.Column<bool>("boolean", nullable: false),
                    SequentialNumber = table.Column<int>("integer", nullable: false),
                    Created = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Version = table.Column<int>("integer", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Billings", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Billings");
        }
    }
}