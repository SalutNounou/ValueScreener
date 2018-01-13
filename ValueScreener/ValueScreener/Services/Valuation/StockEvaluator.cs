using System;
using System.Collections.Generic;
using System.Linq;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public class StockEvaluator : IStockEvaluator
    {
        private readonly IFinancialStatementOrganizer _statementOrganizer;

        public StockEvaluator(IFinancialStatementOrganizer statementOrganizer)
        {
            _statementOrganizer = statementOrganizer;
        }

        public void EvaluateStock(Stock stock)
        {
            if (stock?.MarketData == null) return;
            if (stock.FinancialStatements == null || !stock.FinancialStatements.Any()) return;
            _statementOrganizer.Initialize(stock.FinancialStatements);
            var pricingResults = new PricingResult();
            EvaluateNcaV(pricingResults, stock.MarketData);
            EvaluatePer(pricingResults, stock.MarketData);
            EvaluateEnterpriseMultiple(pricingResults, stock.MarketData);
            pricingResults.AnnualResults = GetAnnualResults();
            stock.PricingResult = pricingResults;
        }

        private void EvaluateEnterpriseMultiple(PricingResult pricingResults, Models.Domain.MarketData stockMarketData)
        {
            var financialStatement = _statementOrganizer.GetLastFinancialStatement();
            if (financialStatement == null) return;
            var marketCap = stockMarketData.MarketCapitalization ?? 0;
            var debt = financialStatement.BalanceSheet.TotalLongTermDebt +
                       financialStatement.BalanceSheet.TotalShortTermDebt;
            var minorityInterest = financialStatement.BalanceSheet.MinorityInterest;
            var cash = financialStatement.BalanceSheet.CashCashEquivalentAndShortTermInvestments;
            var preferredStock = financialStatement.BalanceSheet.PreferredStock;

            var enterpriseValue = marketCap + debt + minorityInterest + preferredStock - cash;
            pricingResults.EnterpriseValue = enterpriseValue;

            var ebitda = financialStatement.IncomeStatement.Ebit +
                         financialStatement.CashFlowStatement.CfDepreciationAmortization;
            if (ebitda >= 0)
            {
                pricingResults.EnterpriseMultiple = enterpriseValue / ebitda;
            }
        }


        private void EvaluatePer(PricingResult pricingResults, Models.Domain.MarketData stockMarketData)
        { 
            var lastStatement = _statementOrganizer.GetLastFinancialStatement();
            if (lastStatement == null) return;
            var revenue = lastStatement.IncomeStatement.TotalRevenue;
            var earnings = lastStatement.IncomeStatement.NetIncomeApplicableToCommon;


            if (stockMarketData.MarketCapitalization.HasValue && earnings > 0)
                pricingResults.PriceEarningRatio = (decimal)(stockMarketData.MarketCapitalization)
                                                   / earnings;
            if (stockMarketData.MarketCapitalization.HasValue && revenue != 0)
                pricingResults.PriceToSalesRatio = (decimal)stockMarketData.MarketCapitalization / revenue;
        }

       

        private void EvaluateNcaV(PricingResult pricingResults, Models.Domain.MarketData marketData)
        {
            var statement = _statementOrganizer.GetLastFinancialStatement();
            if (statement == null) return;
            pricingResults.NetCurrentAssetValue =
                statement.BalanceSheet.TotalCurrentAssets - statement.BalanceSheet.TotalLiabilities;
            if (marketData.MarketCapitalization.HasValue && pricingResults.NetCurrentAssetValue > 0)
            {
                pricingResults.DiscountOnNcav =
                    (1 - (decimal)marketData.MarketCapitalization / pricingResults.NetCurrentAssetValue) * 100;
            }
        }

        private List<AnnualResult> GetAnnualResults()
        {
            var statements = _statementOrganizer.GetAnnualStatements();
            var annualResults = new List<AnnualResult>();
            foreach (var financialStatement in statements)
            {
                var annualResult = new AnnualResult
                {
                    Year = financialStatement.FiscalYear,
                    ReturnOnAssets = CalculateRoa(financialStatement),
                    ReturnOnEquity = CalculateRoe(financialStatement),
                    ReturnOnInvestedCapital = CalculateRoic(financialStatement)
                };
                annualResults.Add(annualResult);
            }
            return annualResults;
        }

        private decimal CalculateRoa(FinancialStatement financialStatement)
        {
            if (financialStatement.IncomeStatement.NetIncomeApplicableToCommon <= 0) return 0;
            if (financialStatement.BalanceSheet.TotalAssets <= 0) return 0;
            return financialStatement.IncomeStatement.NetIncomeApplicableToCommon / financialStatement.BalanceSheet.TotalAssets * 100;
        }


        private decimal CalculateRoe(FinancialStatement financialStatement)
        {
            if (financialStatement.IncomeStatement.NetIncomeApplicableToCommon <= 0) return 0;
            if (financialStatement.BalanceSheet.TotalStockHolderEquity <= 0) return 0;
            return financialStatement.IncomeStatement.NetIncomeApplicableToCommon / financialStatement.BalanceSheet.TotalStockHolderEquity * 100;

        }

        private decimal CalculateRoic(FinancialStatement financialStatement)
        {
            var incomeBeforeTaxes = financialStatement.IncomeStatement.IncomeBeforeTaxes;
            if (incomeBeforeTaxes <= 0) return 0;
            var taxExpense = financialStatement.IncomeStatement.TaxExpense;
            var taxRate = taxExpense / incomeBeforeTaxes;
            var nopat = financialStatement.IncomeStatement.Ebit * (1 - taxRate);
            //var totalAssets = financialStatement.BalanceSheet.TotalAssets;
            //var nonDebtCurrentLiabilities = financialStatement.BalanceSheet.AccountPayables +
            //                                financialStatement.BalanceSheet.RealOtherCurrentAssets;
            var investedCapital = financialStatement.BalanceSheet.TotalStockHolderEquity +
                                  financialStatement.BalanceSheet.TotalLongTermDebt +
                                  financialStatement.BalanceSheet.TotalShortTermDebt;
            var marketableSecurities = Math.Max(0,
                financialStatement.BalanceSheet.CashCashEquivalentAndShortTermInvestments -
                financialStatement.BalanceSheet.CashAndCashEquivalent);
            investedCapital -= marketableSecurities;
            if (investedCapital <= 0) return 0;
            return nopat / investedCapital * 100;
        }



    }
}