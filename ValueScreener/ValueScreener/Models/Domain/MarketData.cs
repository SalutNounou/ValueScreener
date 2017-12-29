using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ValueScreener.Models.Domain
{
    public class MarketData
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal? LastPrice { get; set; }
        public QuotationUnit QuotationUnit { get; set; }    
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal? MarketCapitalization { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}",NullDisplayText = "To Be Refreshed")]
        public Int64? OutstandingShares { get; set; }
       

    }

    public enum QuotationUnit
    {
        InCurrency,
        InHundredth
    }
}