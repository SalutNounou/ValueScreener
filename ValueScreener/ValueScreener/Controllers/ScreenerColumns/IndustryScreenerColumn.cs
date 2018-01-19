using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class IndustryScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.IndustryDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.Industry,
                StockId = stock.Id
            };
        }
    }
}