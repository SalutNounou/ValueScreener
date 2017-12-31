using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;
using ValueScreener.Services.MarketData;

namespace ValueScreener.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMarketDataService _marketDataService;

        public StocksController(ApplicationDbContext context, IMarketDataService marketDataService)
        {
            _context = context;
            _marketDataService = marketDataService;
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.Include(s=>s.MarketData)
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
            var stock = await _context.Stocks.Include(s => s.MarketData)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
           
            var marketData = await _marketDataService.GetMarketDataAsync(stock.Ticker);
            if (marketData != null)
            {
                stock.MarketData = new MarketData
                {
                    LastPrice = marketData.LatestPrice,
                    MarketCapitalization = marketData.MarketCap
                };
                if (marketData.LatestPrice.HasValue && marketData.MarketCap.HasValue)
                    stock.MarketData.OutstandingShares = (long)Math.Floor(marketData.MarketCap.Value / marketData.LatestPrice.Value);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new{id=id});
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
            if (await TryUpdateModelAsync<Stock>(
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
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
