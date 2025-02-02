using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class additionalCompanyFields2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Company",
                newName: "CompanyWeb");

            migrationBuilder.AddColumn<string>(
                name: "CompanyPicUrl",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyTel",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyPicUrl",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CompanyTel",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "CompanyWeb",
                table: "Company",
                newName: "ProfilePicture");
        }
    }
}
