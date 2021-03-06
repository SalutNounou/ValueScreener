﻿using System;
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
            EvaluateFinancialHealth(pricingResults);
            EvaluateMultiples(pricingResults, stock.MarketData);
            EvaluateEnterpriseMultiple(pricingResults, stock.MarketData);
            pricingResults.AnnualResults = GetAnnualResults();
            PerformReturnsStatistics(pricingResults);
            pricingResults.PiotroskiResults = GetPiotroskiResults();
            PerformPiotroskiStatistics(pricingResults);
            EvaluatePriceToFcf(pricingResults, stock.MarketData);
            stock.PricingResult = pricingResults;
        }

        private void EvaluatePriceToFcf(PricingResult pricingResults, Models.Domain.MarketData marketData)
        {
            if (!pricingResults.AnnualResults.Any()) return;
            var lastResult = pricingResults.AnnualResults.OrderByDescending(x => x.Year).First();
            var fcf = lastResult.FreeCashFlow;
            if (fcf != 0) pricingResults.CurrentPriceToFcfRatio = (marketData.MarketCapitalization ?? 0 )/ fcf;
            if (pricingResults.AverageFreeCashFlow == 0) return;
            pricingResults.AveragePriceToFcfRatio =
                (marketData.MarketCapitalization ?? 0) / pricingResults.AverageFreeCashFlow;
        }

        private void EvaluateFinancialHealth(PricingResult pricingResults)
        {
            var statement = _statementOrganizer.GetLastFinancialStatement();
            if (statement == null) return;
            var equity = statement.BalanceSheet.RealTotalEquity;
            var assets = statement.BalanceSheet.TotalAssets;
            var cash = statement.BalanceSheet.CashCashEquivalentAndShortTermInvestments;
            if (assets - cash != 0)
                pricingResults.LeverageRatio = equity * 100 / (assets - cash);
            var interestExpense = statement.IncomeStatement.RealInterestExpense;
            var income = statement.IncomeStatement.Ebit;
            if (interestExpense != 0)
                pricingResults.TimesInterestCovered = income / interestExpense;
        }

        private void PerformReturnsStatistics(PricingResult pricingResults)
        {
            if (pricingResults.AnnualResults.Any())
            {
                pricingResults.AverageRoa = pricingResults.AnnualResults.Average(x => x.ReturnOnAssets);
                pricingResults.AverageRoe = pricingResults.AnnualResults.Average(x => x.ReturnOnEquity);
                pricingResults.AverageRoic = pricingResults.AnnualResults.Average(x => x.ReturnOnInvestedCapital);
                pricingResults.AverageAssetTurnover = pricingResults.AnnualResults.Average(x => x.AssetTurnover);
                pricingResults.AverageCurrentRatio = pricingResults.AnnualResults.Average(x => x.CurrentRatio);
                pricingResults.AverageFreeCashFlow = pricingResults.AnnualResults.Average(x => x.FreeCashFlow);
                pricingResults.AverageGrossMargin = pricingResults.AnnualResults.Average(x => x.GrossMargin);
                pricingResults.AverageLeverage = pricingResults.AnnualResults.Average(x => x.Leverage);
                pricingResults.AverageNetMargin = pricingResults.AnnualResults.Average(x => x.NetMargin);
                pricingResults.AverageQuickRatio = pricingResults.AnnualResults.Average(x => x.QuickRatio);
                var lastResult = pricingResults.AnnualResults.OrderByDescending(x => x.Year).First();
                pricingResults.CurrentRoa = lastResult.ReturnOnAssets;
                pricingResults.CurrentRoe = lastResult.ReturnOnEquity;
                pricingResults.CurrentRoic = lastResult.ReturnOnInvestedCapital;
                pricingResults.CurrentAssetTurnover = lastResult.AssetTurnover;
                pricingResults.CurrentCurrentRatio = lastResult.CurrentRatio;
                pricingResults.CurrentFreeCashFlow = lastResult.FreeCashFlow;
                pricingResults.CurrentGrossMargin = lastResult.GrossMargin;
                pricingResults.CurrentLeverage = lastResult.Leverage;
                pricingResults.CurrentNetMargin = lastResult.NetMargin;
                pricingResults.CurrentQuickRatio = lastResult.QuickRatio;

            }

        }

        private void PerformPiotroskiStatistics(PricingResult pricingResults)
        {
            if (pricingResults.PiotroskiResults.Any())
            {
                var lastResult = pricingResults.PiotroskiResults.OrderByDescending(x => x.Year)
                    .First();
                pricingResults.CurrentPiotroskiScore = lastResult.GlobalFScore;
                pricingResults.AveragePiotroskiScore = (decimal)pricingResults.PiotroskiResults.Average(x => x.GlobalFScore);
                pricingResults.AverageSalesGrowth = pricingResults.PiotroskiResults.Average(x => x.SalesGrowth);
                pricingResults.CurrentSalesGrowth = lastResult.SalesGrowth;
            }
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
            if (ebitda != 0)
            {
                pricingResults.EnterpriseMultiple = enterpriseValue / ebitda;
            }
        }


        private void EvaluateMultiples(PricingResult pricingResults, Models.Domain.MarketData stockMarketData)
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
            if (stockMarketData.MarketCapitalization.HasValue && lastStatement.BalanceSheet.RealTotalEquity != 0)
                pricingResults.PriceToBookRatio = (decimal) stockMarketData.MarketCapitalization /
                                                  lastStatement.BalanceSheet.RealTotalEquity;

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
                    ReturnOnAssets = GetRoa(financialStatement) ?? 0,
                    ReturnOnEquity = CalculateRoe(financialStatement),
                    ReturnOnInvestedCapital = CalculateRoic(financialStatement),
                    AssetTurnover = GetAssetTurnover(financialStatement) ?? 0,
                    CurrentRatio = GetCurrenRatio(financialStatement) ?? 0,
                    QuickRatio = GetQuickRatio(financialStatement) ?? 0,
                    GrossMargin = GetGrossMargin(financialStatement) ?? 0,
                    Leverage = GetEquityLeverage(financialStatement) ?? 0,
                    NetMargin = GetNetMargin(financialStatement) ?? 0,
                    FreeCashFlow = GetFreeCashFlows(financialStatement)
                };
                annualResults.Add(annualResult);
            }
            return annualResults;
        }

        private decimal GetFreeCashFlows(FinancialStatement financialStatement)
        {
            return financialStatement.CashFlowStatement.CashFromOperatingActivities
                   -Math.Abs(financialStatement.CashFlowStatement.CapitalExpanditure);
        }


        private decimal CalculateRoe(FinancialStatement financialStatement)
        {
            if (financialStatement.IncomeStatement.NetIncomeApplicableToCommon <= 0) return 0;
            if (financialStatement.BalanceSheet.RealTotalEquity <= 0) return 0;
            return financialStatement.IncomeStatement.NetIncomeApplicableToCommon / financialStatement.BalanceSheet.RealTotalEquity * 100;

        }

        private decimal CalculateRoic(FinancialStatement financialStatement)
        {
            var incomeBeforeTaxes = financialStatement.IncomeStatement.IncomeBeforeTaxes;
            if (incomeBeforeTaxes <= 0) return 0;
            var taxExpense = financialStatement.IncomeStatement.TaxExpense;
            var taxRate = taxExpense / incomeBeforeTaxes;
            var nopat = financialStatement.IncomeStatement.Ebit * (1 - taxRate);
            var investedCapital = financialStatement.BalanceSheet.RealTotalEquity +
                                  financialStatement.BalanceSheet.TotalLongTermDebt +
                                  financialStatement.BalanceSheet.TotalShortTermDebt;
            var marketableSecurities = Math.Max(0,
                financialStatement.BalanceSheet.CashCashEquivalentAndShortTermInvestments -
                financialStatement.BalanceSheet.CashAndCashEquivalent);
            if (financialStatement.BalanceSheet.CashAndCashEquivalent <= 0) marketableSecurities = 0;
            investedCapital -= marketableSecurities;
            if (investedCapital <= 0) return 0;
            return nopat / investedCapital * 100;
        }

        private List<PiotroskiResult> GetPiotroskiResults()
        {
            var piotroskiResults = new List<PiotroskiResult>();
            var yearFrom = 0;
            var statements = _statementOrganizer.GetConsecutiveAnnualStatementCouple(yearFrom);
            while (statements.Any())
            {
                piotroskiResults.Add(GetPiotroskiResult(statements));
                yearFrom++;
                statements = _statementOrganizer.GetConsecutiveAnnualStatementCouple(yearFrom);
            }
            return piotroskiResults;
        }

        private PiotroskiResult GetPiotroskiResult(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return new PiotroskiResult();
            var result = new PiotroskiResult
            {
                Year = statements[0].FiscalYear,
                PositiveReturns = statements[0].IncomeStatement.NetIncome > 0,
                PositiveOperatingCashFlow = statements[0].CashFlowStatement.CashFromOperatingActivities > 0,
                NoDilutionInShares = false,
                HigherAssetTurnover = HasHigherAssetTurnover(statements),
                GoodAccrual = HasGoodAccrual(statements[0]),
                HigherCurrentRatio = HasHigherCurrentRatio(statements),
                HigherGrossMargin = HasHigherGrossMargin(statements),
                HigherReturnOnAssets = HasHigherReturnOnAssets(statements),
                LowerLeverage = HasLowerLeverage(statements),
                SalesGrowth = GetSalesGrowth(statements)??0
            };

            result.GlobalFScore = (result.GoodAccrual ? 1 : 0)
            + (result.HigherCurrentRatio ? 1 : 0)
            + (result.HigherAssetTurnover ? 1 : 0)
            + (result.HigherGrossMargin ? 1 : 0)
            + (result.HigherReturnOnAssets ? 1 : 0)
            + (result.LowerLeverage ? 1 : 0)
            + (result.NoDilutionInShares ? 1 : 0)
            + (result.PositiveOperatingCashFlow ? 1 : 0)
            + (result.PositiveReturns ? 1 : 0);

            return result;
        }

        private decimal? GetSalesGrowth(List<FinancialStatement> statements)
        {
            if (statements[1].IncomeStatement.TotalRevenue == 0) return null;
            return 100 * ((statements[0].IncomeStatement.TotalRevenue / statements[1].IncomeStatement.TotalRevenue) - 1);
        }

        private bool HasGoodAccrual(FinancialStatement statement)
        {
            return statement.CashFlowStatement.CashFromOperatingActivities >
                   statement.IncomeStatement.NetIncomeApplicableToCommon;
        }

        private bool HasHigherCurrentRatio(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return false;
            var currentRatioThisYear = GetCurrenRatio(statements[0]);
            var currentRatioLastYear = GetCurrenRatio(statements[1]);
            return currentRatioLastYear.HasValue && currentRatioThisYear.HasValue &&
                currentRatioThisYear > currentRatioLastYear;
        }

        private decimal? GetCurrenRatio(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.TotalCurrentLiabilities == 0) return null;
            return 100 * financialStatement.BalanceSheet.TotalCurrentAssets /
                   financialStatement.BalanceSheet.TotalCurrentLiabilities;
        }

        private decimal? GetQuickRatio(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.TotalCurrentLiabilities == 0) return null;
            return 100 * financialStatement.BalanceSheet.CashCashEquivalentAndShortTermInvestments /
                   financialStatement.BalanceSheet.TotalCurrentLiabilities;
        }

        private bool HasHigherAssetTurnover(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return false;
            var assetTurnoverThisYear = GetAssetTurnover(statements[0]);
            var assetTurnoverLastYear = GetAssetTurnover(statements[1]);
            return assetTurnoverLastYear.HasValue && assetTurnoverThisYear.HasValue &&
                assetTurnoverThisYear > assetTurnoverLastYear;
        }

        private decimal? GetAssetTurnover(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.TotalAssets == 0) return null;
            return 100 * financialStatement.IncomeStatement.TotalRevenue / financialStatement.BalanceSheet.TotalAssets;
        }

        private bool HasHigherGrossMargin(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return false;
            var grossMarginThisYear = GetGrossMargin(statements[0]);
            var grossMarginLastYear = GetGrossMargin(statements[1]);
            return grossMarginLastYear.HasValue && grossMarginThisYear.HasValue &&
                grossMarginThisYear > grossMarginLastYear;
        }

        private decimal? GetGrossMargin(FinancialStatement financialStatement)
        {
            if (financialStatement.IncomeStatement.TotalRevenue == 0) return null;
            return 100 * financialStatement.IncomeStatement.GrossProfit / financialStatement.IncomeStatement.TotalRevenue;
        }

        private decimal? GetNetMargin(FinancialStatement financialStatement)
        {
            if (financialStatement.IncomeStatement.TotalRevenue == 0) return null;
            return 100 * financialStatement.IncomeStatement.NetIncome / financialStatement.IncomeStatement.TotalRevenue;
        }

        private bool HasHigherReturnOnAssets(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return false;
            var roaThisYear = GetRoa(statements[0]);
            var roaLastYear = GetRoa(statements[1]);
            return roaLastYear.HasValue && roaThisYear.HasValue &&
                roaThisYear > roaLastYear;
        }

        private decimal? GetRoa(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.TotalAssets == 0) return null;
            return 100 * financialStatement.IncomeStatement.NetIncomeApplicableToCommon /
                   financialStatement.BalanceSheet.TotalAssets;

        }

        private bool HasLowerLeverage(List<FinancialStatement> statements)
        {
            if (statements == null || statements.Count != 2) return false;
            var leverageThisYear = GetLeverage(statements[0]);
            var leverageLastYear = GetLeverage(statements[1]);
            return leverageLastYear.HasValue && leverageThisYear.HasValue &&
                leverageThisYear < leverageLastYear;
        }

        private decimal? GetLeverage(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.TotalAssets == 0) return null;
            return financialStatement.BalanceSheet.TotalLongTermDebt / financialStatement.BalanceSheet.TotalAssets;
        }


        private decimal? GetEquityLeverage(FinancialStatement financialStatement)
        {
            if (financialStatement.BalanceSheet.RealTotalEquity == 0) return null;
            return financialStatement.BalanceSheet.TotalLiabilities / financialStatement.BalanceSheet.RealTotalEquity;
        }
    }
}