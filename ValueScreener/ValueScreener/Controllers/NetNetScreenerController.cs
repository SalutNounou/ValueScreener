
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers
{
    public class NetNetScreenerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NetNetScreenerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? discountMin, bool? includeChineseStocks, bool? includeHealthCareStocks)
        {
            ViewData["Title"] = "Net Net Screener";
            var defaultDiscountMin = discountMin ?? 10;
            defaultDiscountMin = Math.Min(defaultDiscountMin, 100);
            defaultDiscountMin = Math.Max(defaultDiscountMin, 0);
            var defaultIncludeChinesStokcs = includeChineseStocks ?? true;
            var defaultIncludeHealthCareStocks = includeHealthCareStocks ?? true;

            var stocks = await _context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .Where(s => s.MarketData != null
                            && s.MarketData.MarketCapitalization>0
                            && s.PricingResult != null
                            && s.PricingResult.NetCurrentAssetValue>0
                            && s.Sector != "Finance"
                            && s.PricingResult.DiscountOnNcav>= defaultDiscountMin
                            && (defaultIncludeHealthCareStocks || s.Sector!="Health Care")
                            && (defaultIncludeChinesStokcs || s.Country!= "China"))
                            
                            
                            .OrderByDescending(s=> s.PricingResult.DiscountOnNcav).AsNoTracking()
                            .ToListAsync();

            var viewModel = new NetNetScreenerViewModel
            {
                Stocks = stocks,
                DiscountLimit = defaultDiscountMin,
                IncludeChineseStocks = defaultIncludeChinesStokcs,
                IncludeHealthCareStocks = defaultIncludeHealthCareStocks
            };
            //return Content( stocks.Count.ToString());
            return View(viewModel);
        }
    }
}