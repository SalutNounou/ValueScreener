using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class AvgPiotroskiScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.AvgPiotroskiDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            decimal piotroski = 0;
            if (stock.PricingResult != null && stock.PricingResult.PiotroskiResults != null &&
                stock.PricingResult.PiotroskiResults.Any())
                piotroski = (decimal)stock.PricingResult.PiotroskiResults.Average(r => r.GlobalFScore);


            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Number,
                IsBold = false,
                IsLink = false,
                NumberValue = piotroski,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Number;
        }
    }
}