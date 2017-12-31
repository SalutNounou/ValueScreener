using System.Threading.Tasks;

namespace ValueScreener.Services.MarketData
{
    public interface IMarketDataService
    {
        Task<IMarketData> GetMarketDataAsync(string stockTicker);
    }
}


