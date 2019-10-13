using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class addImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image_Path",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Path",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Created_By = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Product_Id = table.Column<Guid>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Product_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_Product_Id",
                table: "Image",
                column: "Product_Id");
        }
    }
}
