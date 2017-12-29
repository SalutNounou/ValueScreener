using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Services.FinancialStatements
{
    public interface IFinancialStatementUpdater
    {
        Task UpdateFinancialStatementsBatchAsync(IQueryable<Stock> stocks, StatementFrequency frequency);
    }
}