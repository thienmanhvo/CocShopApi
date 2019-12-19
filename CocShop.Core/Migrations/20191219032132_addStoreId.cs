using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class addStoreId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Store_Id",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Store_Id",
                table: "Order");
        }
    }
}
