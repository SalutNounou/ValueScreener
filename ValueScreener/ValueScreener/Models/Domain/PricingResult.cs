using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class PricingResult
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        #region Net Nets

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal NetCurrentAssetValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal DiscountOnNcav { get; set; }
        

            #endregion

        #region Multiples

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PriceEarningRatio { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PriceToSalesRatio { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal EnterpriseValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal EnterpriseMultiple { get; set; }


        #endregion



        public List<AnnualResult> AnnualResults { get; set; }
        public List<PiotroskiResult> PiotroskiResults { get; set; }
    }

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
    }

    public class AnnualResult
    {
        public int AnnualResultId { get; set; }
        public int PricingResultId { get; set; }
        public PricingResult PricingResult { get; set; }
        public int Year { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnEquity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ReturnOnInvestedCapital { get; set; }
    }


}