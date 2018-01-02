using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class financialRatios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceEarningRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToSalesRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceEarningRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "PriceToSalesRatio",
                table: "PricingResults");
        }
    }
}
