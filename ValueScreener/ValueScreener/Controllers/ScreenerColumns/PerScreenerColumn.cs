using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class PerScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PerDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal per = 0;
            if (stock.PricingResult != null)
                per = stock.PricingResult.PriceEarningRatio;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = per,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}