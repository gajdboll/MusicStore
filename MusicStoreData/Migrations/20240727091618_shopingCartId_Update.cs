using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Migrations
{
    /// <inheritdoc />
    public partial class shopingCartId_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "DateOfDelete",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ShoppingCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDelete",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
