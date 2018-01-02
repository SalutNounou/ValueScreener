using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class PricingResult
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal NetCurrentAssetValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal EnterpriseValue { get; set; }
        public int PiotroskiScore { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnEquity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnInvestedCapital { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal DiscountOnNcav { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PriceEarningRatio { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PriceToSalesRatio { get; set; }


    }
}