using System.Linq;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class PiotroskiScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.PiotroskiDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            int piotroski = 0;
            if (stock.PricingResult != null && stock.PricingResult.PiotroskiResults != null &&
                stock.PricingResult.PiotroskiResults.Any())
                piotroski = stock.PricingResult.PiotroskiResults.OrderByDescending(x => x.Year).First().GlobalFScore;


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