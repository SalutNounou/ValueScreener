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
    class AvgRoicScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.AverageRoic);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.Country != "China"
                         && s.PricingResult != null
                         && s.PricingResult.PriceToSalesRatio>0
                         && s.PricingResult.CurrentRoic>0
                         && s.PricingResult.AverageRoic<200
                         && s.PricingResult.AnnualResults!=null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Returns On Invested Capital(Avg) Screener";
        public List<string> Columns => new List<string> { ColumnConstants.RoicAvg,ColumnConstants.Roic, ColumnConstants.Per,ColumnConstants.MarketCap,ColumnConstants.Sector};
    }



    class RoicScreener : IScreener
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
            return stocks.OrderByDescending(s => s.PricingResult.CurrentRoic);
        }

        public Expression<Func<Stock, bool>> SelectionCriteria
        {
            get
            {
                return
                    s => s.MarketData != null
                         && s.MarketData.MarketCapitalization > 0
                         && s.Country != "China"
                         && s.PricingResult != null
                         && s.PricingResult.PriceToSalesRatio > 0
                         && s.PricingResult.CurrentRoic > 0
                         && s.PricingResult.AverageRoic < 200
                         && s.PricingResult.AnnualResults != null
                         && s.PricingResult.AnnualResults.Any();
            }
        }
        public string Name => "Returns On Invested Capital Screener";
        public List<string> Columns => new List<string> { ColumnConstants.RoicAvg, ColumnConstants.Roic, ColumnConstants.Per, ColumnConstants.MarketCap, ColumnConstants.Sector };
    }
}