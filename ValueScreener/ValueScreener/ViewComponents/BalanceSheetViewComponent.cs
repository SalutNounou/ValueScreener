using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;
using StatementFrequency = ValueScreener.Models.ViewModels.StatementFrequency;

namespace ValueScreener.ViewComponents
{
    public class BalanceSheetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
           Stock stock, string frequency)
        {
                    
            var viewModel = new FinancialStatementViewModel
            {
                FinancialStatements = stock.FinancialStatements.Where(f=>f.Source==frequency).ToList(),
                Frequency = frequency=="quarterly"?StatementFrequency.Quarterly:StatementFrequency.Annual
            };

            return View(viewModel);
        }
    }
}
