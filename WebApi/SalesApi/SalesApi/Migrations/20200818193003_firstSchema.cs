using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesApi.Migrations
{
    public partial class firstSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerSales",
                columns: table => new
                {
                    CustomerSalesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerType = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    TotalSalesAmount = table.Column<decimal>(nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSales", x => x.CustomerSalesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSales");
        }
    }
}
