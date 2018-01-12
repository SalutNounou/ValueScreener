using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;

namespace ValueScreener.Controllers
{
    public class ScreenerController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ScreenerController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}