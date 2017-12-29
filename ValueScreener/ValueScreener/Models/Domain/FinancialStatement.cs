using System;
using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class FinancialStatement
    {
        public int FinancialStatementId { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public BalanceSheet BalanceSheet { get; set; }
        public IncomeStatement IncomeStatement { get; set; }
        public CashFlowStatement CashFlowStatement { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceivedDate { get; set; }
        public string PeriodLengthCode { get; set; }
        public decimal PeriodLength { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PeriodEnd { get; set; }
        [Required]
        public string FormType { get; set; }
        [Required]
        public int FiscalYear { get; set; }
        public int FiscalQuarter { get; set; }
        public string CurrencyCode { get; set; }
        public decimal UsdConversionRate { get; set; }
        [Required]
        public string PrimarySymbol { get; set; }

        public string Source { get; set; }
        public FinancialStatement()
        {
            BalanceSheet = new BalanceSheet();
            IncomeStatement = new IncomeStatement();
            CashFlowStatement = new CashFlowStatement();
        }
    }

    public enum StatementFrequency
    {
        Annual,
        Quarterly
    }
}