using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalElectricGuitars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAcusticGuitars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalClasicalGuitars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOtherGuitars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBassGuitars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmplifiers = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalStrings = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
