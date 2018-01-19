using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class AvgRoaScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgRoaDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roa = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roa = stock.PricingResult.AnnualResults.Average(r => r.ReturnOnAssets);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roa,
                StockId = stock.Id
            };
        }
    }
}