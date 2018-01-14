using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Controllers.Screeners
{
    public interface IScreener
    {
        IQueryable<Stock> LoadStocks(ApplicationDbContext context);
        IQueryable<Stock> Order(IQueryable<Stock> stocks);
        Expression<Func<Stock,bool>> SelectionCriteria { get; }
        string Name { get; }
        List<string> Columns { get; }
    }
}