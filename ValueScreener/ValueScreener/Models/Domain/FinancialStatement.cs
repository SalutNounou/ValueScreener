using System;
using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.Domain
{
    public class FinancialStatement
    {
        public int FinancialStatementId { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        [Required]
        public string StatementType { get; set; }

        public StatementFrequency Frequency { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Year { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public int YearPeriod { get; set; }

        public BalanceSheet BalanceSheet { get; set; }
        public IncomeStatement IncomeStatement { get; set; }
        public CashFlowStatement CashFlowStatement { get; set; }
    }

    public enum StatementFrequency
    {
        Annual,
        Quarterly
    }
}