using System;
using System.Threading.Tasks;
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
            var marketData = await _marketDataService.GetMarketDataAsync(stock.Ticker);
            if (marketData != null)
            {
                stock.MarketData = new Models.Domain.MarketData
                {
                    LastPrice = marketData.LatestPrice,
                    MarketCapitalization = marketData.MarketCap
                };
                if (marketData.LatestPrice.HasValue && marketData.MarketCap.HasValue)
                    stock.MarketData.OutstandingShares =
                        (long)Math.Floor(marketData.MarketCap.Value / marketData.LatestPrice.Value);
                await context.SaveChangesAsync();
            }
        }
    }
}