using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Delivery_To_Latitude",
                table: "Order",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Delivery_To_Longitude",
                table: "Order",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Delivery_To_Name",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery_To_Latitude",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Delivery_To_Longitude",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Delivery_To_Name",
                table: "Order");
        }
    }
}
