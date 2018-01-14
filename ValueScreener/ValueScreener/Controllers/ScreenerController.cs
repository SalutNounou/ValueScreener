using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Controllers.Screeners;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers
{
    public class ScreenerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IScreenerFactory _screenerFactory;

        public ScreenerController(ApplicationDbContext context, IScreenerFactory screenerFactory)
        {
            _context = context;
            _screenerFactory = screenerFactory;
        }
        public async Task<IActionResult> PriceToSales(int? page)
        {
            ViewData["Title"] = "Price to Sales";
            var stocks = _context.Stocks
                   .Include(s => s.MarketData)
                   .Include(s => s.PricingResult)
                   .Where(s => s.MarketData != null
                               && s.MarketData.MarketCapitalization > 0
                               && s.PricingResult != null
                               && s.PricingResult.PriceToSalesRatio > 0
                               && s.MarketData.MarketCapitalization > 10000000000
                               && s.Sector != "Health Care")
                 .OrderByDescending(s => s.PricingResult.PriceToSalesRatio);
            var pageSize = 25;
            return View(await PaginatedList<Stock>.CreateAsync(stocks.AsNoTracking(),
                page ?? 1, pageSize));
        }


        public async Task<IActionResult> Screen(string criteria, int? page)
        {
            if (string.IsNullOrEmpty(criteria)) return NotFound();
            var screener = _screenerFactory.GetScreener(criteria);
            if (screener == null) return NotFound();
            ViewData["Title"] = screener.Name;
            var stocks = screener.LoadStocks(_context).Where(screener.SelectionCriteria);
            stocks = screener.Order(stocks);
            var pageSize = 25;

            var viewModel = new ScreenerViewModel()
            {
                Stocks = await PaginatedList<Stock>.CreateAsync(stocks.AsNoTracking(), page ?? 1, pageSize)
            };
            return View(viewModel);
        }
    }
}