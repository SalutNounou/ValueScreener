using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class removePricingSimple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterpriseValue",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "PiotroskiScore",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "ReturnOnAssets",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "ReturnOnEquity",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "ReturnOnInvestedCapital",
                table: "PricingResults");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EnterpriseValue",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PiotroskiScore",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnOnAssets",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnOnEquity",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnOnInvestedCapital",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
