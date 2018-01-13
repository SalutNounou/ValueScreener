using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class annualResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EnterpriseMultiple",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EnterpriseValue",
                table: "PricingResults",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AnnualResults",
                columns: table => new
                {
                    AnnualResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PricingResultId = table.Column<int>(nullable: false),
                    ReturnOnAssets = table.Column<decimal>(nullable: false),
                    ReturnOnEquity = table.Column<decimal>(nullable: false),
                    ReturnOnInvestedCapital = table.Column<decimal>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualResults", x => x.AnnualResultId);
                    table.ForeignKey(
                        name: "FK_AnnualResults_PricingResults_PricingResultId",
                        column: x => x.PricingResultId,
                        principalTable: "PricingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualResults_PricingResultId",
                table: "AnnualResults",
                column: "PricingResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualResults");

            migrationBuilder.DropColumn(
                name: "EnterpriseMultiple",
                table: "PricingResults");

            migrationBuilder.DropColumn(
                name: "EnterpriseValue",
                table: "PricingResults");
        }
    }
}
