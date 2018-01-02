using System.Collections.Generic;
using System.Linq;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public class StockEvaluator : IStockEvaluator
    {
        public void EvaluateStock(Stock stock)
        {
            if (stock == null) return;
            if (stock.MarketData == null) return;
            if (stock.FinancialStatements == null || !stock.FinancialStatements.Any()) return;
            var pricingResults = new PricingResult();
            var lastFinancialStatement = stock.FinancialStatements.OrderByDescending(x => x.FiscalYear).First();
            var averageStatement = GetAverageStatement(stock.FinancialStatements);
            EvaluateNcaV(lastFinancialStatement, pricingResults, stock.MarketData);
            EvaluatePER(lastFinancialStatement, pricingResults, stock.MarketData);
            stock.PricingResult = pricingResults;
        }

        private void EvaluatePER(FinancialStatement lastFinancialStatement, PricingResult pricingResults, Models.Domain.MarketData stockMarketData)
        {
            if (lastFinancialStatement.IncomeStatement.NetIncomeApplicableToCommon > 0 &&
                stockMarketData.MarketCapitalization.HasValue)
                pricingResults.PriceEarningRatio = (decimal) (stockMarketData.MarketCapitalization)
                                                   / lastFinancialStatement.IncomeStatement.NetIncomeApplicableToCommon;
            if (stockMarketData.MarketCapitalization.HasValue)
                pricingResults.PriceToSalesRatio = (decimal)stockMarketData.MarketCapitalization/lastFinancialStatement.IncomeStatement.TotalRevenue;
        }

        private FinancialStatement GetAverageStatement(List<FinancialStatement> financialStatements)
        {
            return null;
        }

        private void EvaluateNcaV(FinancialStatement statement, PricingResult pricingResults, Models.Domain.MarketData marketData)
        {
            pricingResults.NetCurrentAssetValue =
                statement.BalanceSheet.TotalCurrentAssets - statement.BalanceSheet.TotalLiabilities;
            if (marketData.MarketCapitalization.HasValue && pricingResults.NetCurrentAssetValue>0)
            {
                pricingResults.DiscountOnNcav =
                    (1 -(decimal) marketData.MarketCapitalization / pricingResults.NetCurrentAssetValue) * 100;
            }
        }
    }
}