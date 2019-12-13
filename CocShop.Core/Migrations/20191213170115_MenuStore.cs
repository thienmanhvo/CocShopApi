using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class MenuStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_Category_Brand_Id",
                table: "Store");

            migrationBuilder.AddColumn<Guid>(
                name: "Cate_Id",
                table: "Store",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Store_Id",
                table: "MenuDish",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_Cate_Id",
                table: "Store",
                column: "Cate_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDish_Store_Id",
                table: "MenuDish",
                column: "Store_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDish_Store_Store_Id",
                table: "MenuDish",
                column: "Store_Id",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_Category_Cate_Id",
                table: "Store",
                column: "Cate_Id",
                principalTable: "Store_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuDish_Store_Store_Id",
                table: "MenuDish");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_Category_Cate_Id",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_Cate_Id",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_MenuDish_Store_Id",
                table: "MenuDish");

            migrationBuilder.DropColumn(
                name: "Cate_Id",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Store_Id",
                table: "MenuDish");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_Category_Brand_Id",
                table: "Store",
                column: "Brand_Id",
                principalTable: "Store_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
