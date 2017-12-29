using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Controllers.Screeners
{
    public class EnterpriseMultipleScreener : IScreener
    {
        public IQueryable<Stock> LoadStocks(ApplicationDbContext context)
        {
            return context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.PiotroskiResults)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.AnnualResults)
                .AsNoTracking();
        }

        public IQueryable<Stock> Order(IQueryable<Stock> stocks)
        {
            return stocks.OrderBy(s => s.PricingResult.EnterpriseMultiple);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.Sector !="Finance"
                         && s.Country !="China"
                         && s.PricingResult != null
                         && s.PricingResult.EnterpriseMultiple>0;
            }
        }
        public string Name => "Enterprise Multiple Screener";
        public List<string> Columns => new List<string> { ColumnConstants.EnterpriseMultiple, ColumnConstants.PiotroskiAvg,  ColumnConstants.Sector, ColumnConstants.Industry, ColumnConstants.Country };
    }
}