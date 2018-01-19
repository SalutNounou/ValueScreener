using System;
using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class PiotroskiResult
    {
        public int PiotroskiResultId { get; set; }
        public int PricingResultId { get; set; }
        public PricingResult PricingResult { get; set; }
        public int Year { get; set; }
        public int GlobalFScore { get; set; }
        public bool PositiveReturns  { get; set; }
        public bool PositiveOperatingCashFlow { get; set; }
        public bool HigherReturnOnAssets { get; set; }
        public bool GoodAccrual { get; set; }
        public bool LowerLeverage { get; set; }
        public bool HigherCurrentRatio { get; set; }
        public bool NoDilutionInShares { get; set; }
        public bool HigherGrossMargin { get; set; }
        public bool HigherAssetTurnover { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal SalesGrowth { get; set; }
    }


    public static class StringHelper
    {
        public static string ToFriendlyString(this Boolean b)
        {
            return b ? "1" : "0";
        }
    }
}