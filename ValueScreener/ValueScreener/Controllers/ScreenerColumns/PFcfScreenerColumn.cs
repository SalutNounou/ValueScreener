using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class PFcfScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PriceToFreeCashFlowDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.CurrentPriceToFcfRatio;
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