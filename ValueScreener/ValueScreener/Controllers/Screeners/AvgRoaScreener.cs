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
    class AvgRoaScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.AverageRoa);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.Country != "China"
                         && s.PricingResult.PriceToSalesRatio>0
                         && s.PricingResult != null
                         && s.PricingResult.AverageRoa < 100
                         && s.PricingResult.AnnualResults != null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Returns On Assets (Avg) Screener";
        public List<string> Columns => new List<string> { ColumnConstants.AvgRoa, ColumnConstants.Roa, ColumnConstants.Per, ColumnConstants.MarketCap, ColumnConstants.Sector };
    }



    class AvgRoeScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.AverageRoe);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.Country != "China"
                         && s.PricingResult.PriceToSalesRatio > 0
                         && s.PricingResult != null
                         && s.PricingResult.AverageRoe >0
                         && s.PricingResult.CurrentRoe >0
                         && s.PricingResult.AnnualResults != null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Returns On Equity (Avg) Screener";
        public List<string> Columns => new List<string> { ColumnConstants.AvgRoe, ColumnConstants.Roe, ColumnConstants.Per, ColumnConstants.MarketCap, ColumnConstants.Sector };
    }
}