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
    public class PiotroskiScreener : IScreener
    {
        public IQueryable<Stock> LoadStocks(ApplicationDbContext context)
        {
            return context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.PiotroskiResults)
                .AsNoTracking();
        }

        public IQueryable<Stock> Order(IQueryable<Stock> stocks)
        {
            return stocks.OrderByDescending(s => s.PricingResult.CurrentPiotroskiScore).ThenByDescending(x=>x.PricingResult.AveragePiotroskiScore);
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
                         && s.PricingResult.PiotroskiResults != null
                         && s.PricingResult.PiotroskiResults.Any();
            }
        }
        public string Name => "Piotroski F-Score Screener";
        public List<string> Columns => new List<string> { ColumnConstants.Piotroski, ColumnConstants.PiotroskiAvg, ColumnConstants.Per,ColumnConstants.MarketCap, ColumnConstants.Sector, ColumnConstants.Industry };
    }


    public class AvgPiotroskiScreener : IScreener
    {
        public IQueryable<Stock> LoadStocks(ApplicationDbContext context)
        {
            return context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.PiotroskiResults)
                .AsNoTracking();
        }

        public IQueryable<Stock> Order(IQueryable<Stock> stocks)
        {
            return stocks.OrderByDescending(s => s.PricingResult.AveragePiotroskiScore);
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
                         && s.PricingResult.PiotroskiResults != null
                         && s.PricingResult.PiotroskiResults.Any();
            }
        }
        public string Name => "Piotroski F-Score (Avg) Screener";
        public List<string> Columns => new List<string> { ColumnConstants.PiotroskiAvg, ColumnConstants.Piotroski, ColumnConstants.Per, ColumnConstants.MarketCap, ColumnConstants.Sector, ColumnConstants.Industry };
    }
}