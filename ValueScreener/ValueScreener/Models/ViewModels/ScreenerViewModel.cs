using System.Collections.Generic;

namespace ValueScreener.Models.ViewModels
{
    public class ScreenerViewModel
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }

        public List<string> ColumnTitles { get; }
        public List<ScreenerRowViewModel> Rows { get; }

        public ScreenerViewModel(int pageIndex, int totalPages, bool hasPreviousPage, bool hasNextPage)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            ColumnTitles = new List<string>();
            Rows = new List<ScreenerRowViewModel>();
        }

     
    }
}
