using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class TickerScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.TickerDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = true,
                StringValue = stock.Ticker,
                StockId = stock.Id
            };
        }
    }
}