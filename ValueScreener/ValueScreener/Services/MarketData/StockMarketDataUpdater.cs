using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.MarketData
{
    public class StockMarketDataUpdater : IStockMarketDataUpdater
    {
        private readonly IMarketDataService _marketDataService;

        public StockMarketDataUpdater(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService;
        }

        public async Task UpdateStockMarketDataAsync(Stock stock, ApplicationDbContext context)
        {
            var marketData = await _marketDataService.GetMarketDataAsync(stock.Ticker.Trim());
            if (marketData != null)
            {
                stock.MarketData = new Models.Domain.MarketData
                {
                    LastPrice = marketData.LatestPrice,
                    MarketCapitalization = marketData.MarketCap,
                };
                if (marketData.LatestPrice.HasValue && marketData.MarketCap.HasValue)
                    stock.MarketData.OutstandingShares =
                        (long)Math.Floor(marketData.MarketCap.Value / marketData.LatestPrice.Value);
                stock.MarketDataSuccess = true;
                stock.MarketDataReceivedDate = DateTime.Today;

            }
            else
            {
                stock.MarketDataSuccess = false;
                stock.MarketDataReceivedDate = DateTime.Today;
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateMarketDataBatchAsync(IQueryable<Stock> stocks)
        {
            var batchSize = 100;
            var count = await stocks.CountAsync();
            Dictionary<string, IMarketData> marketData = new Dictionary<string, IMarketData>();
            var batchNumber = (int)Math.Ceiling(count / (double)batchSize);
            var tasks = new List<Task>();
            for (var ithBatch = 1; ithBatch <= batchNumber; ithBatch++)
            {
                var tickers = BuildTickers(stocks, ithBatch, batchNumber, batchSize);
                tasks.Add(
                      Task.Run(async () =>
                      {
                          var entries = await _marketDataService.GetMarketDataBatchAsync(tickers);
                          foreach (var entry in entries)
                          {
                              if (!marketData.ContainsKey(entry.Key))
                                  marketData.Add(entry.Key, entry.Value);
                          }
                      }));
            }
            await Task.WhenAll(tasks.ToArray());
            foreach (var stock in stocks)
            {
                UpdateStockMarketData(marketData, stock);
            }
        }

        private static List<string> BuildTickers(IQueryable<Stock> stocks, int ithBatch, int batchNumber, int batchSize)
        {
            List<string> tickers;
            if (ithBatch == batchNumber)
            {
                tickers = stocks.Skip((ithBatch - 1) * batchSize).Select(x => x.Ticker.Trim().ToLower()).ToList();
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

        private static void UpdateStockMarketData(Dictionary<string, IMarketData> marketData, Stock stock)
        {
            if (marketData.ContainsKey(stock.Ticker.Trim().ToLower()))
            {
                stock.MarketData = new Models.Domain.MarketData
                {
                    LastPrice = marketData[stock.Ticker.Trim().ToLower()].LatestPrice,
                    MarketCapitalization = marketData[stock.Ticker.Trim().ToLower()].MarketCap,
                };
                if (marketData[stock.Ticker.Trim().ToLower()].LatestPrice.HasValue &&
                    marketData[stock.Ticker.Trim().ToLower()].MarketCap.HasValue)
                    stock.MarketData.OutstandingShares =
                        (long)Math.Floor(marketData[stock.Ticker.Trim().ToLower()].MarketCap.Value /
                                          marketData[stock.Ticker.Trim().ToLower()].LatestPrice.Value);
                stock.MarketDataSuccess = true;
                stock.MarketDataReceivedDate = DateTime.Today;
            }
            else
            {
                stock.MarketDataSuccess = false;
                stock.MarketDataReceivedDate = DateTime.Today;
            }
        }
    }
}