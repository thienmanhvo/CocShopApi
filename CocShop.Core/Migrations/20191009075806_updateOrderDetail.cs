using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocShop.Data.Migrations
{
    public partial class updateOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.CreateTable(
                name: "Order_Detail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Order_Id = table.Column<Guid>(nullable: false),
                    Product_Id = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<double>(nullable: true),
                    Price = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Order_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Product_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Order_Id",
                table: "Order_Detail",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Product_Id",
                table: "Order_Detail",
                column: "Product_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Detail");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Created_By = table.Column<string>(nullable: true),
                    Order_Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: true),
                    Product_Id = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<double>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    Updated_By = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Order_Id",
                table: "OrderDetail",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Product_Id",
                table: "OrderDetail",
                column: "Product_Id");
        }
    }
}
