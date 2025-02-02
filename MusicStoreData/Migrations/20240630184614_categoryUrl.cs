using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class categoryUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategorMoreInfo",
                table: "ProductSubCategory",
                newName: "SubCategoryMoreInfo");

            migrationBuilder.RenameColumn(
                name: "SubCategorMoreInfo",
                table: "ProductCategory",
                newName: "CategoryMoreInfo");

            migrationBuilder.AlterColumn<bool>(
                name: "ProductStatus",
                table: "ProductType",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategoryMoreInfo",
                table: "ProductSubCategory",
                newName: "SubCategorMoreInfo");

            migrationBuilder.RenameColumn(
                name: "CategoryMoreInfo",
                table: "ProductCategory",
                newName: "SubCategorMoreInfo");

            migrationBuilder.AlterColumn<bool>(
                name: "ProductStatus",
                table: "ProductType",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
