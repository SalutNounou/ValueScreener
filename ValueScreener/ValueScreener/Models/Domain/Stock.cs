using System.Collections.Generic;
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
        public PricingResult PricingResult { get; set; }
        public List<FinancialStatement> FinancialStatements { get; set; }
    }

 
}
