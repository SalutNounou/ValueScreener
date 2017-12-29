using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class BasicModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: false),
                    Industry = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Sector = table.Column<string>(nullable: true),
                    Ticker = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatements",
                columns: table => new
                {
                    FinancialStatementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Frequency = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    StatementType = table.Column<string>(nullable: false),
                    StockId = table.Column<int>(nullable: false),
                    Year = table.Column<DateTime>(nullable: false),
                    YearPeriod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatements", x => x.FinancialStatementId);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastPrice = table.Column<decimal>(nullable: false),
                    MarketCapitalization = table.Column<decimal>(nullable: false),
                    OutstandingShares = table.Column<int>(nullable: false),
                    QuotationUnit = table.Column<int>(nullable: false),
                    StockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketDatas_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricingResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnterpriseValue = table.Column<decimal>(nullable: false),
                    NetCurrentAssetValue = table.Column<decimal>(nullable: false),
                    PiotroskiScore = table.Column<int>(nullable: false),
                    ReturnOnAssets = table.Column<decimal>(nullable: false),
                    ReturnOnEquity = table.Column<decimal>(nullable: false),
                    ReturnOnInvestedCapital = table.Column<decimal>(nullable: false),
                    StockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingResults_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BalanceSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FinancialStatementId = table.Column<int>(nullable: false),
                    TotalAssets = table.Column<decimal>(nullable: false),
                    TotalEquity = table.Column<decimal>(nullable: false),
                    TotalLiabilities = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceSheets_FinancialStatements_FinancialStatementId",
                        column: x => x.FinancialStatementId,
                        principalTable: "FinancialStatements",
                        principalColumn: "FinancialStatementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashFlowStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChangeInCashAndCashEquivalent = table.Column<decimal>(nullable: false),
                    FinancialStatementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashFlowStatements_FinancialStatements_FinancialStatementId",
                        column: x => x.FinancialStatementId,
                        principalTable: "FinancialStatements",
                        principalColumn: "FinancialStatementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FinancialStatementId = table.Column<int>(nullable: false),
                    TotalEarnings = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeStatements_FinancialStatements_FinancialStatementId",
                        column: x => x.FinancialStatementId,
                        principalTable: "FinancialStatements",
                        principalColumn: "FinancialStatementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheets_FinancialStatementId",
                table: "BalanceSheets",
                column: "FinancialStatementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowStatements_FinancialStatementId",
                table: "CashFlowStatements",
                column: "FinancialStatementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_StockId",
                table: "FinancialStatements",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatements_FinancialStatementId",
                table: "IncomeStatements",
                column: "FinancialStatementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketDatas_StockId",
                table: "MarketDatas",
                column: "StockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingResults_StockId",
                table: "PricingResults",
                column: "StockId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheets");

            migrationBuilder.DropTable(
                name: "CashFlowStatements");

            migrationBuilder.DropTable(
                name: "IncomeStatements");

            migrationBuilder.DropTable(
                name: "MarketDatas");

            migrationBuilder.DropTable(
                name: "PricingResults");

            migrationBuilder.DropTable(
                name: "FinancialStatements");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
