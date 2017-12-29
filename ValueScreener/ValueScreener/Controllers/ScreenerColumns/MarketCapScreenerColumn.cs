using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class MarketCapScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.MarketCapDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal cap = 0;
            if (stock.MarketData != null)
                cap = stock.MarketData.MarketCapitalization??0;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = cap,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}