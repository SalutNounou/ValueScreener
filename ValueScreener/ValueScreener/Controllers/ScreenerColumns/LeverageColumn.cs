using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class LeverageColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.LeverageDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.CurrentLeverage;
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = ratio,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}