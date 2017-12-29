using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class AvgCurrentRatioScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgCurrentRatioDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.AverageCurrentRatio;
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = ratio,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Percentage;
        }
    }
}