using Microsoft.EntityFrameworkCore.Migrations;

namespace Khoaluan.Migrations
{
    public partial class RefundV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_OrderID",
                table: "Refund",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Order_OrderID",
                table: "Refund",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Order_OrderID",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_Refund_OrderID",
                table: "Refund");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Refund");
        }
    }
}
