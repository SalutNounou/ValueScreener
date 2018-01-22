using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public interface IScreenerColumn
    {
        string DisplayName { get; }
        ScreenerCellViewModel GetCell(Stock stock);
        CellKind GetCellKind();
    }
}