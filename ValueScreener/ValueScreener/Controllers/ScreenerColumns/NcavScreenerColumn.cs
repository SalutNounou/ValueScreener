using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class NcavScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.NcavDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal ncav = 0;
            if (stock.PricingResult != null)
                ncav = stock.PricingResult.NetCurrentAssetValue;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = ncav,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}