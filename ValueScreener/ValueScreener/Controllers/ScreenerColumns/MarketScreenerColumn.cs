using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    class MarketScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.CurrencyDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.QuotationPlace,
                StockId = stock.Id
            };
        }
    }
}