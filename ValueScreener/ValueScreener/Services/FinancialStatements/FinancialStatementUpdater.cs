using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.FinancialStatements
{
    public class FinancialStatementUpdater : IFinancialStatementUpdater
    {
        private readonly IFinancialStatementService _financialStatementService;

        public FinancialStatementUpdater(IFinancialStatementService financialStatementService)
        {
            _financialStatementService = financialStatementService;
        }

        public async Task UpdateFinancialStatementsBatchAsync(IQueryable<Stock> stocks, StatementFrequency frequency)
        {
            var batchSize = 60;
            var count = await stocks.CountAsync();
            List<FinancialStatement> financialStatements = new List<FinancialStatement>();
            var batchNumber = (int)Math.Ceiling(count / (double)batchSize);
            for (var ithBatch = 1; ithBatch <= batchNumber; ithBatch++)
            {
                Thread.Sleep(500);
                var tickers = BuildTickers(stocks, ithBatch, batchNumber, batchSize);
                financialStatements.AddRange(await _financialStatementService.GetFinancialStatementsBatchSafe(tickers,frequency));
            }
            foreach (var stock in stocks)
            {
                UpdateStockFinancialStatements(financialStatements, stock,frequency);
            }
        }

        private void UpdateStockFinancialStatements(List<FinancialStatement> financialStatements, Stock stock, StatementFrequency frequency)
        {
            if (financialStatements.Any(f => f.PrimarySymbol.ToLower().Trim() == stock.Ticker.ToLower().Trim()))
            {
                var sourceToDelete = frequency == StatementFrequency.Annual ? "annual" : "quarterly";
                stock.FinancialStatements.RemoveAll(f => f.Source == sourceToDelete);

                var newStatements = financialStatements.Where(f =>
                    f.PrimarySymbol.ToLower().Trim() == stock.Ticker.ToLower().Trim()).OrderByDescending(f=>f.FiscalYear).ThenByDescending(f=>f.FiscalQuarter).ToList();
                if (frequency == StatementFrequency.Quarterly)
                    newStatements = newStatements.Take(4).ToList();
                stock.FinancialStatements.AddRange(newStatements);
            }
        }

        private static List<string> BuildTickers(IQueryable<Stock> stocks, int ithBatch, int batchNumber, int batchSize)
        {
            List<string> tickers;
            if (ithBatch == batchNumber)
            {
                tickers = stocks.Skip((ithBatch - 1) * batchSize).Select(x => x.Ticker.Trim().ToLower()).ToList();
                if (tickers.Count > batchSize) tickers = tickers.Take(batchSize).ToList();
            }
            else
            {
                tickers = stocks.Skip((ithBatch - 1) * batchSize)
                    .Take(batchSize)
                    .Select(x => x.Ticker.Trim().ToLower())
                    .ToList();
            }
            return tickers;
        }
    }
}