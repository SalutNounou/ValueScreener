using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;
using ValueScreener.Services.Valuation;

namespace ValueScreener.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;
        private readonly IFinancialStatementService _financialStatementService;
        private readonly IStockEvaluator _stockEvaluator;

        public StocksController(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater, IFinancialStatementService financialStatementService, IStockEvaluator stockEvaluator)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
            _financialStatementService = financialStatementService;
            _stockEvaluator = stockEvaluator;
        }

        // GET: Stocks
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
          
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TickerParm"] =
                String.IsNullOrEmpty(sortOrder) ? "Ticker_desc" : "";
            ViewData["NameParm"] =
                sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SectorParm"] =
                sortOrder == "Sector" ? "Sector_desc" : "Sector";
            ViewData["IndustryParm"] =
                sortOrder == "Industry" ? "Industry_desc" : "Industry";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;



            var stocks = from s in _context.Stocks
                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                stocks = stocks.Where(s => s.Ticker.Contains(searchString)
                                               || s.Name.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Ticker";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                stocks = stocks.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                stocks = stocks.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            int pageSize = 25;
            return View(await PaginatedList<Stock>.CreateAsync(stocks.AsNoTracking(),
                page ?? 1, pageSize));
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id, string tab, string frequency)
        {
            ViewData["activeTab"] = tab;
            ViewData["frequency"] = frequency;

            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.AsNoTracking()
                .Include(s=>s.MarketData)
                .Include(s => s.FinancialStatements)
                    .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                    .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                    .ThenInclude(f => f.CashFlowStatement)
                .Include(s => s.PricingResult)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
           
          
            return View(stock);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefreshMarketData(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks
                .Include(s => s.MarketData)
                
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            try
            {
                await _stockMarketDataUpdater.UpdateStockMarketDataAsync(stock, _context);
            }
            catch (DbUpdateException )
            {                
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return RedirectToAction(nameof(Details), new {id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefreshFinancialStatements(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks
                .Include(s => s.FinancialStatements)

                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            try
            {
                var statements = await _financialStatementService.GetFinancialStatementsAsync(stock.Ticker);
                if (statements != null && statements.Any())
                {
                    stock.FinancialStatements = statements;
                    stock.FinancialStatements.AddRange(await _financialStatementService.GetQuarterlyFinancialStatementAsync(stock.Ticker));
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return RedirectToAction(nameof(Details), new {id, tab= "financials" });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefreshValuationCalculation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.BalanceSheet)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.IncomeStatement)
                .Include(s => s.FinancialStatements)
                .ThenInclude(f => f.CashFlowStatement)
                .Include(s=>s.PricingResult)

                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            try
            {
                _stockEvaluator.EvaluateStock(stock);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return RedirectToAction(nameof(Details), new { id, tab = "valuation" });
        }


        // GET: Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ticker,Name,Sector,Industry,Country,Currency,QuotationPlace")] Stock stock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stock);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int ?id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stockToUpdate = await _context.Stocks.SingleOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync(
                stockToUpdate,
                "",
                s => s.Ticker, s => s.Name, s => s.Country, s=>s.Currency, s=>s.QuotationPlace, s=>s.Industry, s=>s.Sector))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
            }
            return View(stockToUpdate);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new {id, saveChangesError = true });
            }
        }

       
    }
}
