namespace ValueScreener.Services.MarketData
{
    public interface IMarketData
    {
        string Symbol { get; set; }
        decimal? LatestPrice { get; set; }
        decimal? MarketCap { get; set; }
    }
}