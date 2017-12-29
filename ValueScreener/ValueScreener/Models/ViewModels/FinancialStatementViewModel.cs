using System.Collections.Generic;
using ValueScreener.Models.Domain;

namespace ValueScreener.Models.ViewModels
{
    public class FinancialStatementViewModel
    {
        public List<FinancialStatement> FinancialStatements { get; set; }
        public StatementFrequency Frequency { get; set; }
    }

   
}