using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.Domain;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;
using ValueScreener.Services.Valuation;

namespace ValueScreener.Services.Batch
{
    public interface IApplicationBatchService
    {
        Task RetrieveAllMArketData();
        Task RetrieveAllFinancialStatements();

        Task ReevaluateAllStocks();
        Task RetrieveEverything();

    }

    public class ApplicationBatchService : IApplicationBatchService
    {
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly ApplicationDbContext _context;
        private readonly IFinancialStatementUpdater _financialStatementUpdater;
        private readonly IStockEvaluator _stockEvaluator;

        public ApplicationBatchService(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater, IFinancialStatementUpdater financialStatementUpdater, IStockEvaluator stockEvaluator)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
            _financialStatementUpdater = financialStatementUpdater;
            _stockEvaluator = stockEvaluator;
        }



        public async Task RetrieveAllMArketData()
        {

            //var minId = await _context.Stocks.MinAsync(x => x.Id);
            //var maxId = await _context.Stocks.MaxAsync(x => x.Id);
            //var batchSize = 200;
            //var batchNumber = (int)Math.Ceiling((decimal)(maxId - minId + 1) / batchSize);
            //for (int i = 1; i <= batchNumber; i++)
            //{
            //    await RetrieveMarketData(i, batchSize, minId, maxId);
            //}
            foreach (var contextMarketData in _context.MarketDatas)
            {
                _context.Remove(contextMarketData);
            }
            await _context.SaveChangesAsync();

            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement);
               // Where(x => x.Id >= idFrom && x.Id <= idTo);
            await _stockMarketDataUpdater.UpdateMarketDataBatchAsync(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task RetrieveMarketData(int whichBatch, int batchSize, int minId, int maxId)
        {
            var idFrom = minId + (whichBatch - 1) * batchSize;
            var idTo = minId + (whichBatch * batchSize) - 1;
            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement).
                Where(x => x.Id >= idFrom && x.Id <= idTo);
            await _stockMarketDataUpdater.UpdateMarketDataBatchAsync(stocks);
            await _context.SaveChangesAsync();
        }

        public async Task RetrieveAllFinancialStatements()
        {
            if (DateTime.Today.Day % 5 != 1) return;

            var minId = await _context.Stocks.MinAsync(x => x.Id);
            var maxId = await _context.Stocks.MaxAsync(x => x.Id);
            var batchSize = 60;
            var batchNumber = (int)Math.Ceiling((decimal)(maxId - minId + 1) / batchSize);
            for (int i = 1; i <= batchNumber; i++)
            {
                await RetrieveAllFinancialStatements(i, batchSize, minId, maxId, StatementFrequency.Annual);
                await RetrieveAllFinancialStatements(i, batchSize, minId, maxId, StatementFrequency.Quarterly);
            }
        }

        public async Task RetrieveAllFinancialStatements(int whichBatch, int batchSize, int minId, int maxId, StatementFrequency frequency)
        {
            var idFrom = minId + (whichBatch - 1) * batchSize;
            var idTo = minId + (whichBatch * batchSize) - 1;
            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement).
                Where(x => x.Id >= idFrom && x.Id <= idTo);
            await _financialStatementUpdater.UpdateFinancialStatementsBatchAsync(stocks, frequency);
            await _context.SaveChangesAsync();
        }

        public async Task ReevaluateAllStocks()
        {

            var minId = await _context.Stocks.MinAsync(x => x.Id);
            var maxId = await _context.Stocks.MaxAsync(x => x.Id);
            var batchSize = 60;
            var batchNumber = (int)Math.Ceiling((decimal)(maxId - minId + 1) / batchSize);
            for (int i = 1; i <= batchNumber; i++)
            {
                await EvaluateStocks(i, batchSize, minId, maxId);
            }
        }

        public async Task EvaluateStocks(int whichBatch, int batchSize, int minId, int maxId)
        {
            var idFrom = minId + (whichBatch - 1) * batchSize;
            var idTo = minId + (whichBatch * batchSize) - 1;
            var stocks = _context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.AnnualResults)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.PiotroskiResults)
                .Where(x => x.Id >= idFrom && x.Id <= idTo);
            await stocks.ForEachAsync(stock => _stockEvaluator.EvaluateStock(stock));
            await _context.SaveChangesAsync();
            Console.WriteLine($"Evaluated Successfuly stocks from id {minId} to {maxId}.");
        }

        public async Task RetrieveEverything()
        {
           // await RetrieveAllMArketData();
            await RetrieveAllFinancialStatements();
            await ReevaluateAllStocks();

        }


        public static readonly Dictionary<int, List<char>> StockGroups = new Dictionary<int, List<char>>
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
