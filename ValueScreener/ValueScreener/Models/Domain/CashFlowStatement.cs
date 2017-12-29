using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class CashFlowStatement
    {
        public int Id { get; set; }
        public int FinancialStatementId { get; set; }
        public FinancialStatement FinancialStatement { get; set; }




       

       

       
       

        
      
      
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal AccountingChange { get; set; }
      
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal TotalAdjustments { get; set; }



        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CfDepreciationAmortization { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ChangeInAccountReceivable { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ChangeInInventories { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ChangeInCurrentAsset { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal ChangeInCurrentLiabilities { get; set; }

       

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CashFromOperatingActivities { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CapitalExpanditure { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal InvestmentChangesNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CashFromInvestingActivities { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal DividendsPaid { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal CashFromFinancingActivities { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal EffectOfExchangeRateOnCash { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", NullDisplayText = "To Be Refreshed")]
        public decimal NetChangeInCash { get; set; }
    }
}