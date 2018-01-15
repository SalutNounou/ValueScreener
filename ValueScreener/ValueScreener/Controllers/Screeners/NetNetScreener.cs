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
    public class NetNetScreener : IScreener
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
                   && s.PricingResult.PriceToSalesRatio > 0
                   && s.PricingResult.PriceToSalesRatio < 10
                   && s.Sector != "Finance"
                   && s.Country != "China"
                   && s.PricingResult.DiscountOnNcav >= 0;

            }
        }

        public string Name => "Net-Net Screener";

        public List<string> Columns => new List<string> { ColumnConstants.NcavDiscount, ColumnConstants.MarketCap, ColumnConstants.Ncav, ColumnConstants.Industry, ColumnConstants.Country };
    }
}