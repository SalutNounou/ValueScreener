using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class PriceToSalesScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PriceToSalesDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = stock.PricingResult.PriceToSalesRatio,
                StockId = stock.Id
            };
        }
    }
}