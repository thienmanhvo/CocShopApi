using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class BithdayAndGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Bithday",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bithday",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");
        }
    }
}
