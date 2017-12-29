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
    public class RoaScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.CurrentRoa);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.PricingResult != null
                         && s.Country != "China"
                         && s.PricingResult.PriceToSalesRatio > 0
                         && s.PricingResult.AnnualResults != null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Return On Assets Screener";
        public List<string> Columns => new List<string> { ColumnConstants.Roa, ColumnConstants.AvgRoa, ColumnConstants.Per, ColumnConstants.Sector, ColumnConstants.Industry };
    }


    public class RoeScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.CurrentRoe);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.PricingResult != null
                         && s.Country != "China"
                         && s.PricingResult.PriceToSalesRatio > 0
                         && s.PricingResult.CurrentRoe> 0
                         && s.PricingResult.AnnualResults != null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Return On Equity Screener";
        public List<string> Columns => new List<string> { ColumnConstants.Roe, ColumnConstants.AvgRoe, ColumnConstants.Per, ColumnConstants.MarketCap,ColumnConstants.Sector };
    }
}