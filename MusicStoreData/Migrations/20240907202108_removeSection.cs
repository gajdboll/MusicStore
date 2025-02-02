using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class removeSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlActions_ControlSection_ControlSectionId",
                table: "ControlActions");

            migrationBuilder.DropTable(
                name: "ControlSection");

            migrationBuilder.DropIndex(
                name: "IX_ControlActions_ControlSectionId",
                table: "ControlActions");

            migrationBuilder.DropColumn(
                name: "ControlSectionId",
                table: "ControlActions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ControlSectionId",
                table: "ControlActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ControlSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlSection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlActions_ControlSectionId",
                table: "ControlActions",
                column: "ControlSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlActions_ControlSection_ControlSectionId",
                table: "ControlActions",
                column: "ControlSectionId",
                principalTable: "ControlSection",
                principalColumn: "Id");
        }
    }
}
