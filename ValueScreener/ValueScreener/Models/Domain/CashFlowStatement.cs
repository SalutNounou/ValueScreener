namespace ValueScreener.Models.Domain
{
    public class CashFlowStatement
    {
        public int Id { get; set; }
        public int FinancialStatementId { get; set; }
        public FinancialStatement FinancialStatement { get; set; }
        public decimal ChangeInCashAndCashEquivalent { get; set; }
      
    }
}