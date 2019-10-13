using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class AddIsDeleteToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_Delete",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_Delete",
                table: "User");
        }
    }
}
