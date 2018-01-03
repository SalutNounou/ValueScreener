using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.ViewComponents
{
    public class IncomeStatementViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            Stock stock, string frequency)
        {
            var viewModel = new FinancialStatementViewModel
            {
                FinancialStatements = stock.FinancialStatements.Where(f => f.Source == frequency).ToList(),
                Frequency = frequency == "quarterly" ? Models.ViewModels.StatementFrequency.Quarterly : Models.ViewModels.StatementFrequency.Annual
            };

            return View(viewModel);
        }
    }
}
