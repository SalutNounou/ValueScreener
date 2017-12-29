using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class piotroski : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PiotroskiResults",
                columns: table => new
                {
                    PiotroskiResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GlobalFScore = table.Column<int>(nullable: false),
                    GoodAccrual = table.Column<bool>(nullable: false),
                    HigherAssetTurnover = table.Column<bool>(nullable: false),
                    HigherCurrentRatio = table.Column<bool>(nullable: false),
                    HigherGrossMargin = table.Column<bool>(nullable: false),
                    HigherReturnOnAssets = table.Column<bool>(nullable: false),
                    LowerLeverage = table.Column<bool>(nullable: false),
                    NoDilutionInShares = table.Column<bool>(nullable: false),
                    PositiveOperatingCashFlow = table.Column<bool>(nullable: false),
                    PositiveReturns = table.Column<bool>(nullable: false),
                    PricingResultId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiotroskiResults", x => x.PiotroskiResultId);
                    table.ForeignKey(
                        name: "FK_PiotroskiResults_PricingResults_PricingResultId",
                        column: x => x.PricingResultId,
                        principalTable: "PricingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PiotroskiResults_PricingResultId",
                table: "PiotroskiResults",
                column: "PricingResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PiotroskiResults");
        }
    }
}
