using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.Domain;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;


namespace ValueScreener.Controllers
{
    public class RefreshDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly IFinancialStatementUpdater _financialStatementUpdater;

        public RefreshDataController(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater, IFinancialStatementUpdater financialStatementUpdater)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
            _financialStatementUpdater = financialStatementUpdater;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RefreshMarketData()
        {
            var stocks =  _context.Stocks.Include(s => s.MarketData).OrderBy(s => s.Ticker);
            try
            {
                await _stockMarketDataUpdater.UpdateMarketDataBatchAsync(stocks);               
                await _context.SaveChangesAsync();    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Content("Failure : " + e.Message + ((e.InnerException != null) ? e.InnerException.Message : String.Empty));
            }
             return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMarketData()
        {
            foreach (var contextMarketData in _context.MarketDatas)
            {
                _context.Remove(contextMarketData);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RefreshAnnualFinancialStatements()
        {
            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement).OrderBy(s => s.Ticker);
            try
            {
                await _financialStatementUpdater.UpdateFinancialStatementsBatchAsync(stocks,StatementFrequency.Annual);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Content("Failure : " + e.Message + ((e.InnerException != null) ? e.InnerException.Message : String.Empty));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFinancialStatements()
        {
            foreach (var financialStatement in _context.FinancialStatements)
            {
                _context.Remove(financialStatement);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
