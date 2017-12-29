namespace ValueScreener.Models.Domain
{
    public class MarketData
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        
        public decimal LastPrice { get; set; }
        public QuotationUnit QuotationUnit { get; set; }
        public decimal MarketCapitalization { get; set; }
        public int OutstandingShares { get; set; }
        public string QuotationPlace { get; set; }

    }

    public enum QuotationUnit
    {
        InCurrency,
        InHundredth
    }
}