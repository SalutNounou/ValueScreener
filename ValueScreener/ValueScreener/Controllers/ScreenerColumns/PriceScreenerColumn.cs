using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class PriceScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PriceDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            var price = 0M;
            if (stock.MarketData != null)
                price = stock.MarketData.LastPrice ?? 0;
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = price,
                StockId = stock.Id
            };
        }
    }
}