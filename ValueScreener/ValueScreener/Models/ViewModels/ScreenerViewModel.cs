using System.Collections.Generic;

namespace ValueScreener.Models.ViewModels
{
    public class ScreenerViewModel
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }

        public List<ColumnTitle> ColumnTitles { get; }
        public List<ScreenerRowViewModel> Rows { get; }

        public ScreenerViewModel(int pageIndex, int totalPages, bool hasPreviousPage, bool hasNextPage)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            ColumnTitles = new List<ColumnTitle>();
            Rows = new List<ScreenerRowViewModel>();
            AvailableAdditionalColumns = new Dictionary<string, string>();
        }

        public Dictionary<string, string> AvailableAdditionalColumns { get; }

     
    }

    public class ColumnTitle
    {
        public string Title { get; set; }
        public string columnId { get; set; }
        public bool IsSticky { get; set; }
    }

}
