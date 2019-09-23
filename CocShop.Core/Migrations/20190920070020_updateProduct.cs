using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_Category_Cate_Id",
                table: "Product");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Sale",
                table: "Product",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Sale",
                table: "Product",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Is_New",
                table: "Product",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Best",
                table: "Product",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<Guid>(
                name: "Cate_Id",
                table: "Product",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_Category_Cate_Id",
                table: "Product",
                column: "Cate_Id",
                principalTable: "Product_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_Category_Cate_Id",
                table: "Product");

            migrationBuilder.AlterColumn<double>(
                name: "Price_Sale",
                table: "Product",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Sale",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_New",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Best",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Cate_Id",
                table: "Product",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_Category_Cate_Id",
                table: "Product",
                column: "Cate_Id",
                principalTable: "Product_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
