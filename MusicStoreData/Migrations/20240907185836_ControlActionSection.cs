using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class ControlActionSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "ControlSection",
                table: "ControlActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ControlSectionId",
                table: "ControlActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControlerUrl",
                table: "ControlActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ControlSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfDelete = table.Column<DateTime>(type: "datetime2", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ControlSection",
                table: "ControlActions");

            migrationBuilder.DropColumn(
                name: "ControlSectionId",
                table: "ControlActions");

            migrationBuilder.DropColumn(
                name: "ControlerUrl",
                table: "ControlActions");

            migrationBuilder.InsertData(
                table: "ControlActions",
                columns: new[] { "Id", "DateAdded", "DateOfDelete", "Descriptions", "ModificationDate", "Name", "Position", "isActive" },
                values: new object[,]
                {
                    { 1, null, null, "Web Header", null, "WebHeader", 1, true },
                    { 2, null, null, "Product Category", null, "ProductCategory", 2, true },
                    { 3, null, null, "ProductSubCategory", null, "ProductSubCategory", 3, true },
                    { 4, null, null, "ProductType", null, "ProductType", 4, true },
                    { 5, null, null, "Color", null, "Color", 5, true },
                    { 6, null, null, "Manufacturer", null, "Manufacturer", 6, true },
                    { 7, null, null, "News And Articles", null, "NewsAndArticle", 7, true },
                    { 8, null, null, "Links", null, "FooterInfoLinks", 8, true },
                    { 9, null, null, "Terms & Conditions", null, "TermsAndCondition", 9, true },
                    { 10, null, null, "Action Controllers", null, "ControlActions", 10, true }
                });
        }
    }
}
