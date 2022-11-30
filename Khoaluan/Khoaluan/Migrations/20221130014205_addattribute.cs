using Microsoft.EntityFrameworkCore.Migrations;

namespace Khoaluan.Migrations
{
    public partial class addattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Item",
                newName: "MinPrice");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPrice",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Passwork",
                table: "Developer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Developer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Passwork",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Developer");

            migrationBuilder.RenameColumn(
                name: "MinPrice",
                table: "Item",
                newName: "Quantity");
        }
    }
}
