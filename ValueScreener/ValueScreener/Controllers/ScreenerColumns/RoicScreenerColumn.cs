using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class RoicScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.RoicDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roic = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roic = stock.PricingResult.AnnualResults.OrderByDescending(x => x.Year).First().ReturnOnInvestedCapital;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = roic,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Percentage;
        }
    }
}