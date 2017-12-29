namespace ValueScreener.Models.Domain
{
    public class IncomeStatement
    {
        public int Id { get; set; }
        public int FinancialStatementId { get; set; }
        public FinancialStatement FinancialStatement { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}