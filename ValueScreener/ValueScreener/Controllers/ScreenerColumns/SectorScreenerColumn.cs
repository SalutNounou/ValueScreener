using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class SectorScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.SectorDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = false,
                StringValue = stock.Sector,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Text;
        }
    }
}