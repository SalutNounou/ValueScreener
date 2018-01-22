using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class NcavDiscountScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.NcavDiscountDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Percentage,
                IsBold = false,
                IsLink = false,
                PercentageValue = stock.PricingResult.DiscountOnNcav,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Percentage;
        }
    }
}