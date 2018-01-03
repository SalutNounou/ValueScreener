using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Services.MarketData;

namespace ValueScreener.Services.Batch
{
    public interface IApplicationBatchService
    {
        Task RetrieveAllMArketData();
    }

    public class ApplicationBatchService : IApplicationBatchService
    {
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly ApplicationDbContext _context;

        public ApplicationBatchService(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
        }



        public async Task RetrieveAllMArketData()
        {
            var stocks = _context.Stocks.Include(s => s.MarketData).OrderBy(s => s.Ticker);

            await _stockMarketDataUpdater.UpdateMarketDataBatchAsync(stocks);
            await _context.SaveChangesAsync();
        }
    }
}
