using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class MenuDish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Menu_Id",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuDish",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Is_Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDish", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Menu_Id",
                table: "Product",
                column: "Menu_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_MenuDish_Menu_Id",
                table: "Product",
                column: "Menu_Id",
                principalTable: "MenuDish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_MenuDish_Menu_Id",
                table: "Product");

            migrationBuilder.DropTable(
                name: "MenuDish");

            migrationBuilder.DropIndex(
                name: "IX_Product_Menu_Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Menu_Id",
                table: "Product");
        }
    }
}
