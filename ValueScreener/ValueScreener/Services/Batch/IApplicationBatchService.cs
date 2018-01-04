using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.Domain;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;

namespace ValueScreener.Services.Batch
{
    public interface IApplicationBatchService
    {
        Task RetrieveAllMArketData();
        Task RetrieveAllFinancialStatements(int whichDecile,StatementFrequency frequency);
        
    }

    public class ApplicationBatchService : IApplicationBatchService
    {
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly ApplicationDbContext _context;
        private readonly IFinancialStatementUpdater _financialStatementUpdater;

        public ApplicationBatchService(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater, IFinancialStatementUpdater financialStatementUpdater)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
            _financialStatementUpdater = financialStatementUpdater;
        }



        public async Task RetrieveAllMArketData()
        {
            var stocks = _context.Stocks.Include(s => s.MarketData).OrderBy(s => s.Ticker);

            await _stockMarketDataUpdater.UpdateMarketDataBatchAsync(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task RetrieveAllFinancialStatements(int whichDecile, StatementFrequency frequency)
        {
            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement).Where(stock=>StockGroups[whichDecile].Any(letter=>stock.Ticker.ToUpper().StartsWith(letter)));
            await _financialStatementUpdater.UpdateFinancialStatementsBatchAsync(stocks,frequency);
            await _context.SaveChangesAsync();
        }

     


        public static readonly  Dictionary<int, List<char>> StockGroups = new Dictionary<int, List<char>>
        {
            {1,new List<char>{'A'} },
            {2,new List<char>{'B'} },
            {3,new List<char>{'C','D'} },
            {4,new List<char>{'E','F'} },
            {5,new List<char>{'G','H','I'} },
            {6,new List<char>{'K','L'} },
            {7,new List<char>{'M','N'} },
            {8,new List<char>{'O','P','Q','R'} },
            {9,new List<char>{'S','T'} },
            {10,new List<char>{'U','V','X','Y','Z'} },
        };
    }
}
