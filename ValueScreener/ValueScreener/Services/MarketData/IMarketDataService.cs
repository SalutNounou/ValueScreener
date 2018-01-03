using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.MarketData
{
    public interface IMarketDataService
    {
        Task<IMarketData> GetMarketDataAsync(string stockTicker);
        Task<Dictionary<string, IMarketData>> GetMarketDataBatchAsync(List<string> tickers);
       

    }
}


