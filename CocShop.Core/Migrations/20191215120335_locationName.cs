using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class locationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location_Name",
                table: "Store",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total_Store",
                table: "Store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_Name",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Total_Store",
                table: "Store");
        }
    }
}
