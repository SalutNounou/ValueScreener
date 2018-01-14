using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Controllers.Screeners
{
    public class NetNetScreener : IScreener
    {
        public IQueryable<Stock> LoadStocks(ApplicationDbContext context)
        {
            return context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult).AsNoTracking();
        }

        public IQueryable<Stock> Order(IQueryable<Stock> stocks)
        {
            return stocks.OrderByDescending(s => s.PricingResult.DiscountOnNcav);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
              s => s.MarketData != null
                   && s.MarketData.MarketCapitalization > 0
                   && s.PricingResult != null
                   && s.PricingResult.NetCurrentAssetValue > 0
                   && s.Sector != "Finance"
                   && s.PricingResult.DiscountOnNcav >= 0;

            }
        }

        public string Name { get { return "Net-Net Screener"; } }
    }
}