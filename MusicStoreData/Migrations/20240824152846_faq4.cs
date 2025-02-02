using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class faq4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechInfo_ProductType_ProductTypeId",
                table: "TechInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechInfo",
                table: "TechInfo");

            migrationBuilder.RenameTable(
                name: "TechInfo",
                newName: "TechInfos");

            migrationBuilder.RenameIndex(
                name: "IX_TechInfo_ProductTypeId",
                table: "TechInfos",
                newName: "IX_TechInfos_ProductTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechInfos",
                table: "TechInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechInfos_ProductType_ProductTypeId",
                table: "TechInfos",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechInfos_ProductType_ProductTypeId",
                table: "TechInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechInfos",
                table: "TechInfos");

            migrationBuilder.RenameTable(
                name: "TechInfos",
                newName: "TechInfo");

            migrationBuilder.RenameIndex(
                name: "IX_TechInfos_ProductTypeId",
                table: "TechInfo",
                newName: "IX_TechInfo_ProductTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechInfo",
                table: "TechInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechInfo_ProductType_ProductTypeId",
                table: "TechInfo",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
