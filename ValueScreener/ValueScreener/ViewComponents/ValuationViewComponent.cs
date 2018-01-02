using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;
using ValueScreener.Services.Valuation;

namespace ValueScreener.ViewComponents
{
    public class ValuationViewComponent : ViewComponent
    {
        private readonly IValuationHintAnalyzer _hintAnalyzer;

        public ValuationViewComponent(IValuationHintAnalyzer hintAnalyzer)
        {
            _hintAnalyzer = hintAnalyzer;
        }

        public async Task<IViewComponentResult> InvokeAsync(
            Stock stock)
        {
            var viewModel = new ValuationViewModel(){
                PricingResult = stock.PricingResult,
                Currency = stock.Currency,
                Id = stock.Id,
                Hints = _hintAnalyzer.GiveHints(stock.PricingResult)
            };
            return View(viewModel);
        }
    }
}