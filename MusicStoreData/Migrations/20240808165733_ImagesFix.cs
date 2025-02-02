using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class ImagesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImage",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ProductType_ProductTypeId",
                table: "ProductImage",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_ProductType_ProductTypeId",
                table: "ProductImage");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "ProductImage",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductTypeId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
