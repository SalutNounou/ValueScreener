using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValueScreener.Models.Domain;

namespace ValueScreener.Models.ViewModels
{
    public class ValuationViewModel
    {
        public PricingResult PricingResult { get; set; }
        public string Currency { get; set; }
        public int Id { get; set; }
        public Dictionary<string, Hint> Hints { get; set; }
    }

    public class Hint
    {
        public string Message { get; set; }
        public AlertLevel Level { get; set; }
    }

    public enum AlertLevel
    {
        Success,
        Info,
        Warning,
        Danger
    }
}
