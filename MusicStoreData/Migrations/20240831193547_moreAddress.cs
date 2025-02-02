using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class moreAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumer",
                table: "MusicStoreAddress",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "MusicStoreAddress",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "MusicStoreAddress",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "MusicStoreAddress");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "MusicStoreAddress");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "MusicStoreAddress",
                newName: "PhoneNumer");
        }
    }
}
