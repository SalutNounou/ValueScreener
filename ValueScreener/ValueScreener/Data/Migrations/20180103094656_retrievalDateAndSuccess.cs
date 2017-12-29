using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class retrievalDateAndSuccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnnualStatementsReceivedDate",
                table: "Stocks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "AnnualStatementsSuccess",
                table: "Stocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketDataReceivedDate",
                table: "Stocks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "MarketDataSuccess",
                table: "Stocks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuarterlyStatementsReceivedDate",
                table: "Stocks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "QuarterlyStatementsSuccess",
                table: "Stocks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualStatementsReceivedDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "AnnualStatementsSuccess",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "MarketDataReceivedDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "MarketDataSuccess",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "QuarterlyStatementsReceivedDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "QuarterlyStatementsSuccess",
                table: "Stocks");
        }
    }
}
