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
    public class PriceToSalesScreener : IScreener
    {
        public string Name { get { return "Price to Sales Screener"; } }
        public List<string> Columns=> new List<string>{ColumnConstants.PriceToSales,ColumnConstants.MarketCap, ColumnConstants.Sector,ColumnConstants.Industry, ColumnConstants.Country};

        public IQueryable<Stock> LoadStocks(ApplicationDbContext context)
        {
            return context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult).AsNoTracking();
        }

      

        public IQueryable<Stock> Order(IQueryable<Stock> stocks)
        {
            return stocks.OrderByDescending(s => s.PricingResult.PriceToSalesRatio);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return s => s.MarketData != null
                            && s.MarketData.MarketCapitalization > 0
                            && s.PricingResult != null
                            && s.PricingResult.PriceToSalesRatio > 0
                            && s.Country != "China"
                            && s.Sector != "Health Care"
                            && s.MarketData.MarketCapitalization>1000000000;
            }
        }
    }
}