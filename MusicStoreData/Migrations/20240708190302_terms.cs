using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class terms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlActions",
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
                    table.PrimaryKey("PK_ControlActions", x => x.Id);
                });

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
                    { 10, null, null, "Action Controllers", null, "ActionControllers", 10, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlActions");
        }
    }
}
