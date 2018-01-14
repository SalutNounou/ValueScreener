using System.ComponentModel.DataAnnotations;

namespace ValueScreener.Models.ViewModels
{
    public class ScreenerCellViewModel
    {
        public CellKind CellKind { get; set; }
        public bool IsBold { get; set; }
        public bool IsLink { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal PercentageValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal NumberValue { get; set; }
        public string StringValue { get; set; }
        public int StockId { get; set; }
        
    }


    public enum CellKind
    {
        Number,
        Percentage,
        Text,
        Nothing
    }
}
