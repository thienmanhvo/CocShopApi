using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Order_Detail");

            migrationBuilder.DropColumn(
                name: "Is_Delete",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Order_Detail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "Total_Price",
                table: "Order_Detail",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total_Price",
                table: "Order",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Price",
                table: "Order_Detail");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Order_Detail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Order_Detail",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total_Price",
                table: "Order",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Delete",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }
    }
}
