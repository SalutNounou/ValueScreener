namespace ValueScreener.Models.Domain
{
    public class PricingResult
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        
        public decimal NetCurrentAssetValue { get; set; }
        public decimal EnterpriseValue { get; set; }
        public int PiotroskiScore { get; set; }
        public decimal ReturnOnAssets { get; set; }
        public decimal ReturnOnEquity { get; set; }
        public decimal ReturnOnInvestedCapital { get; set; }
    }
}