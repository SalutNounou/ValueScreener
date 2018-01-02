using System.Collections.Generic;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Services.Valuation
{
    public interface IValuationHintAnalyzer
    {
        Dictionary<string, Hint> GiveHints(PricingResult pricingResult);
    }

    public class ValuationHintAnalyzer : IValuationHintAnalyzer
    {
        public Dictionary<string, Hint> GiveHints(PricingResult pricingResult)
        {
            var hints = new Dictionary<string, Hint>();
            if (pricingResult == null) return hints;
            if (pricingResult.DiscountOnNcav >= 30)
            {
                hints.Add("NCAV", new Hint { Level = AlertLevel.Success, Message = "The Discount to Net Current Asset Value is equal to " + pricingResult.DiscountOnNcav + "%. A discount superior to 30% is often an indicator that the stock is undervalued." });
            }
            if (pricingResult.PriceEarningRatio > 0 && pricingResult.PriceEarningRatio <= 10)
            {
                hints.Add("PER", new Hint { Level = AlertLevel.Success, Message = "The Price To Earnings Ratio is equal to " + pricingResult.PriceEarningRatio + ". A P/E ratio inferior to 10 is often an indicator that the stock is undervalued." });
            }
            if (pricingResult.PriceEarningRatio > 25)
            {
                hints.Add("PER", new Hint { Level = AlertLevel.Warning, Message = "The Price To Earnings Ratio is equal to " + pricingResult.PriceEarningRatio + ". A P/E ratio superior to 25 is often an indicator that the stock may be overvalued." });
            }
            if (pricingResult.PriceToSalesRatio > 8)
            {
                hints.Add("PS", new Hint { Level = AlertLevel.Danger, Message = "The Price To Sales Ratio is equal to " + pricingResult.PriceToSalesRatio + ". A P/S ratio superior to 8 is often an indicator that the stock is strongly overvalued." });
            }
            return hints;
        }
    }
}