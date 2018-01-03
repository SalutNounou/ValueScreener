using System.Collections.Generic;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.FinancialStatements
{
    public interface IFinancialStatementService
    {
        Task<List<FinancialStatement>> GetFinancialStatementsAsync(string stockTicker);
        Task<IEnumerable<FinancialStatement>> GetQuarterlyFinancialStatementAsync(string stockTicker);
    }
}
