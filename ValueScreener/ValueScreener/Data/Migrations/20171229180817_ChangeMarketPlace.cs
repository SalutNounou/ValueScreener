using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class ChangeMarketPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuotationPlace",
                table: "MarketDatas");

            migrationBuilder.AddColumn<string>(
                name: "QuotationPlace",
                table: "Stocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuotationPlace",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "QuotationPlace",
                table: "MarketDatas",
                nullable: true);
        }
    }
}
