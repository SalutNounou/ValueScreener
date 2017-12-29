using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.MarketData
{
    public interface IStockMarketDataUpdater
    {
        Task UpdateStockMarketDataAsync(Stock stock, ApplicationDbContext context);
        Task UpdateMarketDataBatchAsync(IQueryable<Stock> stocks);
       
    }
}