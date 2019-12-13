using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class StoreCate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store_Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Is_Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store_Category", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Store_Category_Brand_Id",
                table: "Store",
                column: "Brand_Id",
                principalTable: "Store_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Store_Category_Brand_Id",
                table: "Store");

            migrationBuilder.DropTable(
                name: "Store_Category");
        }
    }
}
