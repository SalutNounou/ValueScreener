using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class CompanyNameScreenerColumn : IScreenerColumn
    {
        public string DisplayName => ColumnConstants.CompanyNameDisplay;
        public ScreenerCellViewModel GetCell(Stock stock)
        {
            return new ScreenerCellViewModel
            {
                CellKind = CellKind.Text,
                IsBold = false,
                IsLink = true,
                StringValue = stock.Name,
                StockId = stock.Id
            };
        }

        public CellKind GetCellKind()
        {
            return CellKind.Text;
        }
    }
}