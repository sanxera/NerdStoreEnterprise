using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Order.Infra.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MySequence",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", nullable: false),
                    Percent = table.Column<decimal>(nullable: true),
                    ValueDiscount = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UtilizationDate = table.Column<DateTime>(nullable: false),
                    ValidateDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Used = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropSequence(
                name: "MySequence");
        }
    }
}
