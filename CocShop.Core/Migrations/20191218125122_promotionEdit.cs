using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class promotionEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Store_Store_Id",
                table: "Promotion");

            migrationBuilder.DropIndex(
                name: "IX_Promotion_Store_Id",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Store_Id",
                table: "Promotion");

            migrationBuilder.AlterColumn<double>(
                name: "Discount_Percent",
                table: "Promotion",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Discount_Percent",
                table: "Promotion",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Store_Id",
                table: "Promotion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Store_Id",
                table: "Promotion",
                column: "Store_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Store_Store_Id",
                table: "Promotion",
                column: "Store_Id",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
