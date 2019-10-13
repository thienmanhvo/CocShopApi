using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateCardNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Is_Delete",
                table: "Payment_Method",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Card_Number",
                table: "Payment_Method",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Delete",
                table: "Location",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Is_Delete",
                table: "Payment_Method",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "Card_Number",
                table: "Payment_Method",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Delete",
                table: "Location",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
