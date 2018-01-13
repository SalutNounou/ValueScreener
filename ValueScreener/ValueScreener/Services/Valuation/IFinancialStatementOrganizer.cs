using System.Collections.Generic;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.Valuation
{
    public interface IFinancialStatementOrganizer
    {
        void Initialize(List<FinancialStatement> stockFinancialStatements);
        FinancialStatement GetLastFinancialStatement();
        FinancialStatement GetLastQuarterlyFinancialStatement();
        List<FinancialStatement> GetAnnualStatements();
        List<FinancialStatement> GetQuarterlyStatements();
        List<FinancialStatement> GetConsecutiveAnnualStatementCouple(int from);
    }
}