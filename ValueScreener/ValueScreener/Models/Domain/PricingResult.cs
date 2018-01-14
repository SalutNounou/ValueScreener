﻿using System.Collections.Generic;
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
}