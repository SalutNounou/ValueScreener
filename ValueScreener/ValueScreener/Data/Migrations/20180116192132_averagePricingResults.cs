using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class averagePricingResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AveragePiotroskiScore",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRoa",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRoe",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRoic",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPiotroskiScore",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentRoa",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentRoe",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentRoic",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AveragePiotroskiScore",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageRoa",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageRoe",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "AverageRoic",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentPiotroskiScore",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentRoa",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentRoe",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "CurrentRoic",
                table: "PricingResults");
        }
    }
}
