using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class InterestCoveredScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.InterestCoveredDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var ratio = 0M;
            if (stock.PricingResult != null)
                ratio = stock.PricingResult.TimesInterestCovered;
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