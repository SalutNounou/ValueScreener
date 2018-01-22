using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class AvgRoicScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgRoicDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal roic = 0;
            if (stock.PricingResult != null && stock.PricingResult.AnnualResults != null &&
                stock.PricingResult.AnnualResults.Any())
                roic = stock.PricingResult.AnnualResults.Average(r => r.ReturnOnInvestedCapital);


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