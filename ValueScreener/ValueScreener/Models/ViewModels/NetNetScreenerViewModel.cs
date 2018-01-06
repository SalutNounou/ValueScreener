using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Models.ViewModels
{
    public class NetNetScreenerViewModel
    {
        [Range(1,100)]
        public int DiscountLimit { get; set; }

        public bool IncludeHealthCareStocks { get; set; }
        public bool IncludeChineseStocks { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
