using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class techInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    BodyMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeckMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FingerboardMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pickups = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BridgeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Electronics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StringGauge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TuningMachines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FretboardRadius = table.Column<double>(type: "float", nullable: false),
                    NumberOfFrets = table.Column<int>(type: "int", nullable: false),
                    Finish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    AccessoriesIncluded = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechInfos_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechInfos_ProductTypeId",
                table: "TechInfos",
                column: "ProductTypeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechInfos");
        }
    }
}
