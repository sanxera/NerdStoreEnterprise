using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Order.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Used",
                table: "Vouchers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Used",
                table: "Vouchers",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
