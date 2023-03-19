using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Data.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndexNo = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryExitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterControllerId = table.Column<int>(type: "int", nullable: false),
                    DoorControllerId = table.Column<int>(type: "int", nullable: false),
                    SpecialFunctionId = table.Column<int>(type: "int", nullable: false),
                    Leavedt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events",
                schema: "dbo");
        }
    }
}
