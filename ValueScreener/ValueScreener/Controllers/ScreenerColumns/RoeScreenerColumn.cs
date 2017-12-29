using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class RoeScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoeDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roe = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roe = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnEquity;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roe,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Percentage;
        }
    }
}