using System.Collections.Generic;

namespace ValueScreener.Models.ViewModels
{
    public class ScreenerRowViewModel
    {
        public int StockId { get; }
        public List<ScreenerCellViewModel> Cells { get; private set; }

        public ScreenerRowViewModel(int stockId)
        {
            StockId = stockId;
            Cells= new List<ScreenerCellViewModel>();
        }
    }
}