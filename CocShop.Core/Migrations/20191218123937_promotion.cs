using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Is_Active = table.Column<bool>(nullable: true),
                    Discount_Percent = table.Column<bool>(nullable: true),
                    Brand_Id = table.Column<Guid>(nullable: true),
                    Store_Id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_Brand_Brand_Id",
                        column: x => x.Brand_Id,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Promotion_Store_Store_Id",
                        column: x => x.Store_Id,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Brand_Id",
                table: "Promotion",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Store_Id",
                table: "Promotion",
                column: "Store_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
