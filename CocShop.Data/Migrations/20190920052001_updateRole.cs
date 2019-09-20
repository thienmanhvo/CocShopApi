using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "User_Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "User_Role",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "User_Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "User_Role",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "Role",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "User_Role");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "User_Role");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "User_Role");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "User_Role");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "Role");
        }
    }
}
