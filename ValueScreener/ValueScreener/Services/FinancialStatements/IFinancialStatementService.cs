using System.Collections.Generic;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;
using ValueScreener.Services.MarketData;

namespace ValueScreener.Services.FinancialStatements
{
    public interface IFinancialStatementService
    {
        Task<List<FinancialStatement>> GetFinancialStatementsAsync(string stockTicker);
        Task<IEnumerable<FinancialStatement>> GetQuarterlyFinancialStatementAsync(string stockTicker);
        Task<IEnumerable<FinancialStatement>> GetFinancialStatementsBatch(List<string> tickers, StatementFrequency frequency);
        Task<IEnumerable<FinancialStatement>> GetFinancialStatementsBatchSafe(List<string> tickers, StatementFrequency frequency);
    }
}
