using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ValueScreener.Data.Migrations
{
    public partial class financialStatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalEarnings",
                table: "IncomeStatements",
                newName: "TotalRevenue");

            migrationBuilder.RenameColumn(
                name: "YearPeriod",
                table: "FinancialStatements",
                newName: "FiscalYear");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "FinancialStatements",
                newName: "ReceivedDate");

            migrationBuilder.RenameColumn(
                name: "StatementType",
                table: "FinancialStatements",
                newName: "PrimarySymbol");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "FinancialStatements",
                newName: "PeriodEnd");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "FinancialStatements",
                newName: "FiscalQuarter");

            migrationBuilder.RenameColumn(
                name: "ChangeInCashAndCashEquivalent",
                table: "CashFlowStatements",
                newName: "TotalAdjustments");

            migrationBuilder.AddColumn<decimal>(
                name: "CostOfRevenue",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscontinuedOperation",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Ebit",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EquityEarnings",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraordinaryItems",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossProfit",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomeBeforeTaxes",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestExpense",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetIncome",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetIncomeApplicableToCommon",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ResearchDevelopementExpense",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingGeneralAdministrativeExpense",
                table: "IncomeStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "FinancialStatements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormType",
                table: "FinancialStatements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PeriodLength",
                table: "FinancialStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PeriodLengthCode",
                table: "FinancialStatements",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UsdConversionRate",
                table: "FinancialStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AccountingChange",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CapitalExpanditure",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashFromFinancingActivities",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashFromInvestingActivities",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashFromOperatingActivities",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CfDepreciationAmortization",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangeInAccountReceivable",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangeInCurrentAsset",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangeInCurrentLiabilities",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangeInInventories",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DividendsPaid",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EffectOfExchangeRateOnCash",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvestmentChangesNet",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetChangeInCash",
                table: "CashFlowStatements",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashAndCashEquivalent",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashCashEquivalentAndShortTermInvestments",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CommonStock",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeferredCharges",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Goodwill",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IntangibleAssets",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InventoriesNet",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinorityInterest",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherAssets",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCurrentAssets",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCurrentLiabilities",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherEquity",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherLiabilities",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreferredStock",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PropertyPlantEquipmentNet",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RetainedEarnings",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCurrentAssets",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCurrentLiabilities",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalLongTermDebt",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalReceivableNet",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalShortTermDebt",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalStockHolderEquity",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TreasuryStock",
                table: "BalanceSheets",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostOfRevenue",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "DiscontinuedOperation",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "Ebit",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "EquityEarnings",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "ExtraordinaryItems",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "GrossProfit",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "IncomeBeforeTaxes",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "InterestExpense",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "NetIncome",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "NetIncomeApplicableToCommon",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "ResearchDevelopementExpense",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "SellingGeneralAdministrativeExpense",
                table: "IncomeStatements");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "FinancialStatements");

            migrationBuilder.DropColumn(
                name: "FormType",
                table: "FinancialStatements");

            migrationBuilder.DropColumn(
                name: "PeriodLength",
                table: "FinancialStatements");

            migrationBuilder.DropColumn(
                name: "PeriodLengthCode",
                table: "FinancialStatements");

            migrationBuilder.DropColumn(
                name: "UsdConversionRate",
                table: "FinancialStatements");

            migrationBuilder.DropColumn(
                name: "AccountingChange",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CapitalExpanditure",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CashFromFinancingActivities",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CashFromInvestingActivities",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CashFromOperatingActivities",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CfDepreciationAmortization",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "ChangeInAccountReceivable",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "ChangeInCurrentAsset",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "ChangeInCurrentLiabilities",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "ChangeInInventories",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "DividendsPaid",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "EffectOfExchangeRateOnCash",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "InvestmentChangesNet",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "NetChangeInCash",
                table: "CashFlowStatements");

            migrationBuilder.DropColumn(
                name: "CashAndCashEquivalent",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "CashCashEquivalentAndShortTermInvestments",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "CommonStock",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "DeferredCharges",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "Goodwill",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "IntangibleAssets",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "InventoriesNet",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "MinorityInterest",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "OtherAssets",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "OtherCurrentAssets",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "OtherCurrentLiabilities",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "OtherEquity",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "OtherLiabilities",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "PreferredStock",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "PropertyPlantEquipmentNet",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "RetainedEarnings",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalCurrentAssets",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalCurrentLiabilities",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalLongTermDebt",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalReceivableNet",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalShortTermDebt",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TotalStockHolderEquity",
                table: "BalanceSheets");

            migrationBuilder.DropColumn(
                name: "TreasuryStock",
                table: "BalanceSheets");

            migrationBuilder.RenameColumn(
                name: "TotalRevenue",
                table: "IncomeStatements",
                newName: "TotalEarnings");

            migrationBuilder.RenameColumn(
                name: "ReceivedDate",
                table: "FinancialStatements",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "PrimarySymbol",
                table: "FinancialStatements",
                newName: "StatementType");

            migrationBuilder.RenameColumn(
                name: "PeriodEnd",
                table: "FinancialStatements",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "FiscalYear",
                table: "FinancialStatements",
                newName: "YearPeriod");

            migrationBuilder.RenameColumn(
                name: "FiscalQuarter",
                table: "FinancialStatements",
                newName: "Frequency");

            migrationBuilder.RenameColumn(
                name: "TotalAdjustments",
                table: "CashFlowStatements",
                newName: "ChangeInCashAndCashEquivalent");
        }
    }
}
