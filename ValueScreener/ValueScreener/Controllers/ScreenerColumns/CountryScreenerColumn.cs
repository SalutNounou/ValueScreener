using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class CountryScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.CountryDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.Country,
                StockId = stock.Id
            };
        }
    }
}