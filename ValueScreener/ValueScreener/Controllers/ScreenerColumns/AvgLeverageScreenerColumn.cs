using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class AvgLeverageScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgLeverageDisplay;

        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.AverageLeverage;
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = ratio,
                StockId = stock.Id
            };
        }
    }
}