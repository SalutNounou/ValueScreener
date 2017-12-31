using System.Threading.Tasks;

namespace ValueScreener.Services.MarketData
{
    public interface IMarketDataService
    {
        Task<IServiceMarketData> GetMarketDataAsync(string stockTicker);
    }
}


