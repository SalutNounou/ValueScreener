using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class AvgNetMarginScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgNetMarginDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.AverageNetMargin;
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