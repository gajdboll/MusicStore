using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class iNFORMATIONlINKSuPDATE3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "NewsAndArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BlogImage",
                table: "NewsAndArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BtnName",
                table: "NewsAndArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewsImage",
                table: "NewsAndArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewsInfo",
                table: "NewsAndArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "NewsAndArticles");

            migrationBuilder.DropColumn(
                name: "BlogImage",
                table: "NewsAndArticles");

            migrationBuilder.DropColumn(
                name: "BtnName",
                table: "NewsAndArticles");

            migrationBuilder.DropColumn(
                name: "NewsImage",
                table: "NewsAndArticles");

            migrationBuilder.DropColumn(
                name: "NewsInfo",
                table: "NewsAndArticles");
        }
    }
}
