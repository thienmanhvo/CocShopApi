using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class BrandStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Image_Path = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Is_Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Image_Path = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Is_Delete = table.Column<bool>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Brand_Id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_Brand_Brand_Id",
                        column: x => x.Brand_Id,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Store_Brand_Id",
                table: "Store",
                column: "Brand_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
