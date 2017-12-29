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
    public class PFcfScreener : IScreener
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
            return stocks.OrderBy(s => s.PricingResult.CurrentPriceToFcfRatio);
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
                         && s.PricingResult.CurrentPriceToFcfRatio > 0

                         && s.Sector != "Finance"
                         && s.Country != "China";
            }
        }

        public string Name => "Price/Free Cash Flows Screener";

        public List<string> Columns => new List<string> { ColumnConstants.PriceToFreeCashFlow, ColumnConstants.AvgRoa,ColumnConstants.MarketCap,ColumnConstants.Sector, ColumnConstants.Country };
    }
}