
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;

namespace ValueScreener.Controllers
{
    public class NetNetScreenerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NetNetScreenerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stocks = await _context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .Where(s => s.MarketData != null
                            && s.MarketData.MarketCapitalization>0
                            && s.PricingResult != null
                            && s.PricingResult.NetCurrentAssetValue>0
                            && s.PricingResult.DiscountOnNcav>0).OrderByDescending(s=> s.PricingResult.DiscountOnNcav).AsNoTracking()
                            .ToListAsync();

            //return Content( stocks.Count.ToString());
            return View(stocks);
        }
    }
}