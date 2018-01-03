﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Services.MarketData;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ValueScreener.Controllers
{
    public class RefreshDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockMarketDataUpdater _stockMarketDataUpdater;

        public RefreshDataController(ApplicationDbContext context, IStockMarketDataUpdater stockMarketDataUpdater)
        {
            _context = context;
            _stockMarketDataUpdater = stockMarketDataUpdater;
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



    }
}
