using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class EnterpriseMultipleScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.EnterpriseMultipleDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal em = 0;
            if (stock.PricingResult != null)
                em = stock.PricingResult.EnterpriseMultiple;


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = em,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}