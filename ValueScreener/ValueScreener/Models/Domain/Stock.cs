using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Ticker's length cannot be longer than 10 characters.")]
        public string Ticker { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Stock Name's length cannot be longer than 200 characters.")]
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
        [Required]
        public string Currency { get; set; }

        public MarketData MarketData { get; set; }
        public DateTime MarketDataReceivedDate { get; set; }
        public bool MarketDataSuccess { get; set; }
        public DateTime AnnualStatementsReceivedDate { get; set; }
        public bool AnnualStatementsSuccess { get; set; }
        public DateTime QuarterlyStatementsReceivedDate { get; set; }
        public bool QuarterlyStatementsSuccess { get; set; }

        public PricingResult PricingResult { get; set; }
        public List<FinancialStatement> FinancialStatements { get; set; }
        [DisplayName("Quotation Place")]
        public string QuotationPlace { get; set; }

        public Stock()
        {
            MarketDataReceivedDate = DateTime.MinValue;
            AnnualStatementsReceivedDate = DateTime.MinValue;
            QuarterlyStatementsReceivedDate = DateTime.MinValue;
        }
    }


}
