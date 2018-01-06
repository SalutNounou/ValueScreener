using System.Collections.Generic;
using System.Linq;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public class StockEvaluator : IStockEvaluator
    {
        public void EvaluateStock(Stock stock)
        {
            if (stock == null) return ;
            if (stock.MarketData == null) return ;
            if (stock.FinancialStatements == null || !stock.FinancialStatements.Any()) return ;
            var pricingResults = new PricingResult();
            var lastFinancialStatement = stock.FinancialStatements.OrderByDescending(x => x.FiscalYear).ThenByDescending(f=>f.FiscalQuarter).First();
            var averageStatement = GetAverageStatement(stock.FinancialStatements);
            EvaluateNcaV(lastFinancialStatement, pricingResults, stock.MarketData);
            EvaluatePER(stock.FinancialStatements, pricingResults, stock.MarketData);
            stock.PricingResult = pricingResults;
        }

        private void EvaluatePER(List<FinancialStatement> financialStatements, PricingResult pricingResults, Models.Domain.MarketData stockMarketData)
        {
            decimal revenue, earnings;
            var quarterlyStatements = financialStatements.Where(x => x.Source == "quarterly").ToList();
            var isReallyQuarterly = quarterlyStatements.Any()&& quarterlyStatements.All(x => x.FormType == "10-Q" || x.FormType == "10-K");
            if (isReallyQuarterly)
            {
                revenue = quarterlyStatements.Sum(x => x.IncomeStatement.TotalRevenue);
                earnings = quarterlyStatements.Sum(x => x.IncomeStatement.NetIncomeApplicableToCommon);
            }
            else
            {
                
                var lastStatement = financialStatements.Where(x => x.Source == "annual")
                    .OrderByDescending(x => x.FiscalYear).ThenByDescending(f => f.FiscalQuarter).FirstOrDefault();
                if (lastStatement == null) return;
                revenue = lastStatement.IncomeStatement.TotalRevenue;
                earnings = lastStatement.IncomeStatement.NetIncomeApplicableToCommon;
            }
                


            if (stockMarketData.MarketCapitalization.HasValue && earnings>0)
                pricingResults.PriceEarningRatio = (decimal) (stockMarketData.MarketCapitalization)
                                                   / (decimal)earnings;
            if (stockMarketData.MarketCapitalization.HasValue && revenue!=0)
                pricingResults.PriceToSalesRatio = (decimal)stockMarketData.MarketCapitalization/(decimal)revenue;
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