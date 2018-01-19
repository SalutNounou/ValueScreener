using System;
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
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal LeverageRatio { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TimesInterestCovered { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CurrentRoa { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal AverageRoa { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CurrentRoe { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal AverageRoe { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CurrentRoic { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal AverageRoic { get; set; }


        public int CurrentPiotroskiScore { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public Decimal AveragePiotroskiScore { get; set; }



        #endregion



        public List<AnnualResult> AnnualResults { get; set; }
        public List<PiotroskiResult> PiotroskiResults { get; set; }
    }
}