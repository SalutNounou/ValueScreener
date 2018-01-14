using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Models.ViewModels
{
    public class ScreenerViewModel
    {
     
        public PaginatedList<Stock> Stocks { get; set; }
    }
}
