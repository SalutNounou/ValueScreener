using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.ViewComponents
{
    public class ScreenerCellViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            ScreenerCellViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}