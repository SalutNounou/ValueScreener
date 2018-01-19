using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class SalesGrowth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesGrowth",
                table: "AnnualResults");

            migrationBuilder.AddColumn<decimal>(
                name: "SalesGrowth",
                table: "PiotroskiResults",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesGrowth",
                table: "PiotroskiResults");

            migrationBuilder.AddColumn<decimal>(
                name: "SalesGrowth",
                table: "AnnualResults",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
