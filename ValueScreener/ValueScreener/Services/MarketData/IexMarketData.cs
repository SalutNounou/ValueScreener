using System;

namespace ValueScreener.Services.MarketData
{
    public class IexMarketData : IMarketData
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryExchange { get; set; }
        public string Sector { get; set; }
        public string CalculationPrice { get; set; }

        public decimal? Open { get; set; }

        public Int64? OpenTime { get; set; }

        public decimal? Close { get; set; }

        public Int64? CloseTime { get; set; }

        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public decimal? LatestPrice { get; set; }

        public string LatestSource { get; set; }

        public string LatestTime { get; set; }

        public Int64? LatestUpdate { get; set; }

        public decimal? LatestVolume { get; set; }

        public decimal? IexRealTimePrice { get; set; }

        public decimal? IexRealTimeSize { get; set; }

        public Int64? IexLastUpdated { get; set; }

        public decimal? DelayedPrice { get; set; }

        public Int64? DelayedPriceTime { get; set; }

        public decimal? PreviousClose { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercent { get; set; }

        public decimal? IexMarketPercent { get; set; }

        public decimal? IexVolume { get; set; }

        public decimal? AvgTotalVolume { get; set; }

        public decimal? IexBidPrice { get; set; }

        public decimal? IexBidSize { get; set; }

        public decimal? IexAskPrice { get; set; }

        public decimal? IexAskSize { get; set; }

        public decimal? MarketCap { get; set; }

        public decimal? PeRatio { get; set; }

        public decimal? Week52Low { get; set; }

        public decimal? YtdChange { get; set; }

    }
}