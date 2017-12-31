namespace ValueScreener.Services.MarketData
{
    public interface IServiceMarketData
    {
        string Symbol { get; set; }
        decimal? LatestPrice { get; set; }
        decimal? MarketCap { get; set; }
    }
}