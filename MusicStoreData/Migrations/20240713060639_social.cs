using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class social : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryMoreInfo",
                table: "SocialMedia",
                newName: "SocialMoreInfo");

            migrationBuilder.UpdateData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "ControlActions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocialMoreInfo",
                table: "SocialMedia",
                newName: "CategoryMoreInfo");

            migrationBuilder.UpdateData(
                table: "ControlActions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "ActionControllers");
        }
    }
}
