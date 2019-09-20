using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Payment_Method",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "Payment_Method",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Payment_Method",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "Payment_Method",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "Notification",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Hub_User_Connection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "Hub_User_Connection",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Hub_User_Connection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "Hub_User_Connection",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Payment_Method");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Payment_Method");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Payment_Method");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "Payment_Method");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Hub_User_Connection");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Hub_User_Connection");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Hub_User_Connection");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "Hub_User_Connection");
        }
    }
}
