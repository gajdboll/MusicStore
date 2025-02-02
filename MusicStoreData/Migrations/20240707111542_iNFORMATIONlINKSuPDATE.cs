using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class iNFORMATIONlINKSuPDATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InformationLink",
                table: "InformationLinks");

            migrationBuilder.RenameColumn(
                name: "LinkDescription",
                table: "InformationLinks",
                newName: "InformationLinksMoreInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InformationLinksMoreInfo",
                table: "InformationLinks",
                newName: "LinkDescription");

            migrationBuilder.AddColumn<string>(
                name: "InformationLink",
                table: "InformationLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
