using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class PriceBookScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PbRatioDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal pbr = 0;
            if (stock.PricingResult != null)
                pbr = stock.PricingResult.PriceToBookRatio;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = pbr,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}