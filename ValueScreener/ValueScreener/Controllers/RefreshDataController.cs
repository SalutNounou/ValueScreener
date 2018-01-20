using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Authorization;
using ValueScreener.Data;
using ValueScreener.Models.Domain;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;
using ValueScreener.Services.Valuation;


namespace ValueScreener.Controllers
{
    public class RefreshDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly IFinancialStatementUpdater _financialStatementUpdater;
        private readonly IStockEvaluator _stockEvaluator;
        private readonly IAuthorizationService _authorizationService;

        public RefreshDataController(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater, IFinancialStatementUpdater financialStatementUpdater, IStockEvaluator stockEvaluator, IAuthorizationService authorizationService)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
            _financialStatementUpdater = financialStatementUpdater;
            _stockEvaluator = stockEvaluator;
            _authorizationService = authorizationService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RefreshMarketData()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            var stocks = _context.Stocks.Include(s => s.MarketData).OrderBy(s => s.Ticker);
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
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            foreach (var contextMarketData in _context.MarketDatas)
            {
                _context.Remove(contextMarketData);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RefreshAnnualFinancialStatements()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            var stocks = _context.Stocks
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement).OrderBy(s => s.Ticker);
            try
            {
                await _financialStatementUpdater.UpdateFinancialStatementsBatchAsync(stocks, StatementFrequency.Annual);
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
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            foreach (var financialStatement in _context.FinancialStatements)
            {
                _context.Remove(financialStatement);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteValuations()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            foreach (var pricingResult in _context.PricingResults)
            {
                _context.Remove(pricingResult);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RefreshPricingResults()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
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
                .ThenInclude(p => p.PiotroskiResults);
            try
            {
                await stocks.ForEachAsync(stock => _stockEvaluator.EvaluateStock(stock));
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
        public async Task<IActionResult> AddNyseStocks()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            DbInitializer.ImportNyseStocks(_context);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AddNasdaqStocks()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            DbInitializer.ImportNasdaqStocks(_context);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AddAmexStocks()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Stock(), StockOperations.Refresh);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            DbInitializer.ImportAmexStocks(_context);
            return RedirectToAction(nameof(Index));

        }

    }
}
