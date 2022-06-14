using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Cart.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InventoryQuantity",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CartClient",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "UsedVoucher",
                table: "CartClient",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "CartClient",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "CartClient",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percent",
                table: "CartClient",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueDiscount",
                table: "CartClient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CartClient");

            migrationBuilder.DropColumn(
                name: "UsedVoucher",
                table: "CartClient");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "CartClient");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "CartClient");

            migrationBuilder.DropColumn(
                name: "Percent",
                table: "CartClient");

            migrationBuilder.DropColumn(
                name: "ValueDiscount",
                table: "CartClient");

            migrationBuilder.AlterColumn<string>(
                name: "InventoryQuantity",
                table: "CartItems",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
