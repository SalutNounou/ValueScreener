using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class ManyRatios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageAssetTurnover",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageCurrentRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageFreeCashFlow",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageGrossMargin",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageLeverage",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageNetMargin",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AveragePriceToFcfRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageQuickRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageSalesGrowth",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentAssetTurnover",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCurrentRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentFreeCashFlow",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentGrossMargin",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentLeverage",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentNetMargin",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPriceToFcfRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentQuickRatio",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentSalesGrowth",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AssetTurnover",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentRatio",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FreeCashFlow",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossMargin",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Leverage",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetMargin",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QuickRatio",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesGrowth",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageAssetTurnover",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageCurrentRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageFreeCashFlow",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageGrossMargin",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageLeverage",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageNetMargin",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AveragePriceToFcfRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageQuickRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageSalesGrowth",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentAssetTurnover",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentCurrentRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentFreeCashFlow",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentGrossMargin",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentLeverage",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentNetMargin",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentPriceToFcfRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentQuickRatio",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentSalesGrowth",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AssetTurnover",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "CurrentRatio",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "FreeCashFlow",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "GrossMargin",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "Leverage",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "NetMargin",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "QuickRatio",
                table: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "SalesGrowth",
                table: "AnnualResults");
        }
    }
}
