using System;
using System.Linq;
using ValueScreener.Models.Domain;

namespace ValueScreener.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Stocks.Any())
            {
                return;   // DB has been seeded
            }

            var stocks = new Stock[]
            {
                new Stock{Country = "United States", Currency = "USD",Industry = "Technology", Name = "Apple Inc", Ticker = "AAPL", Sector = "Technology"},
                new Stock{Country = "United States", Currency = "USD",Industry = "Technology", Name = "Microsoft Corp", Ticker = "MSFT", Sector = "Technology"},
            };

            foreach (var s in stocks)
            {
                context.Stocks.Add(s);
            }
            context.SaveChanges();


           
        }
    }
}
