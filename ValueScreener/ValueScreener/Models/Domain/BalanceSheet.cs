using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValueScreener.Models.Domain
{
    public class BalanceSheet
    {
        public int Id { get; set; }
        public int FinancialStatementId { get; set; }
        public FinancialStatement FinancialStatement { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalLiabilities { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalEquity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CommonStock { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal DeferredCharges { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CashAndCashEquivalent { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CashCashEquivalentAndShortTermInvestments { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal Goodwill { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal IntangibleAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal InventoriesNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal MinorityInterest { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal OtherAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal OtherCurrentAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal OtherCurrentLiabilities { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal OtherEquity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal OtherLiabilities { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PreferredStock { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal PropertyPlantEquipmentNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal RetainedEarnings { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalCurrentAssets { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalCurrentLiabilities { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalLongTermDebt { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalReceivableNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalShortTermDebt { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalStockHolderEquity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TreasuryStock { get; set; }
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal RealOtherCurrentAssets
        {
            get
            {
                if (OtherCurrentAssets > 0) return OtherCurrentAssets;
                return TotalCurrentAssets - CashCashEquivalentAndShortTermInvestments - InventoriesNet -
                       TotalReceivableNet;
            }
        }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal RealDeferredCharges
        {
            get
            {
                if (DeferredCharges > 0) return DeferredCharges;
                return TotalAssets - TotalCurrentAssets - PropertyPlantEquipmentNet - Goodwill - IntangibleAssets - OtherAssets 
                       ;
            }
        }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal AccountPayables
        {
            get { return TotalCurrentLiabilities - TotalShortTermDebt - OtherCurrentLiabilities; }
        }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CapitalSurplus
        {
            get { return TotalStockHolderEquity +CommonStock +PreferredStock - RetainedEarnings - TreasuryStock; }
        }

    }
}