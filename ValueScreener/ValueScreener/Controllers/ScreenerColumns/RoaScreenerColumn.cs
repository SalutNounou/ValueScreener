using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class RoaScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoaDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {

            decimal roa = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roa = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnAssets;


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