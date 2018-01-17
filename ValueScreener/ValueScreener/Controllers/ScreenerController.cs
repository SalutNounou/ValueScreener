using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Controllers.Screeners;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers
{
    public class ScreenerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IScreenerFactory _screenerFactory;
        private readonly IScreenerCellsGenerator _cellsGenerator;

        public ScreenerController(ApplicationDbContext context, IScreenerFactory screenerFactory, IScreenerCellsGenerator cellsGenerator)
        {
            _context = context;
            _screenerFactory = screenerFactory;
            _cellsGenerator = cellsGenerator;
        }
     
        public async Task<IActionResult> Screen(string criteria, int? page,string columns, string columnToAdd, string columnToRemove)
        {
            if (string.IsNullOrEmpty(criteria)) return NotFound();
            var screener = _screenerFactory.GetScreener(criteria);
            if (screener == null) return NotFound();
            if (!ManageColumns(screener, columns, columnToAdd, columnToRemove, out var columnsTodisplay)) return NotFound();
            ViewData["Columns"] = string.Join(',',columnsTodisplay);
            ViewData["Criteria"] = criteria;
            ViewData["Title"] = screener.Name;
           
            var stocks = screener.LoadStocks(_context).Where(screener.SelectionCriteria);
            stocks = screener.Order(stocks);
            var pageSize = 25;

            var pageList = await PaginatedList<Stock>.CreateAsync(stocks.AsNoTracking(), page ?? 1, pageSize);

            var viewModel = new ScreenerViewModel(pageList.PageIndex,pageList.TotalPages,pageList.HasPreviousPage,pageList.HasNextPage);

            foreach (var column in ColumnConstants.Columns.Where(x=>!columnsTodisplay.Contains(x.Key)))
            {
                viewModel.AvailableAdditionalColumns.Add(column.Key,column.Value);
            }

            viewModel.ColumnTitles.AddRange(_cellsGenerator.GetColumnTitles(columnsTodisplay));
            viewModel.Rows.AddRange(_cellsGenerator.GetRows(pageList, columnsTodisplay));
            return View(viewModel);
        }


        private bool ManageColumns(IScreener screener, string columns, string columnToAdd, string columnToRemove, out List<string> columnsTodisplay)
        {
            columnsTodisplay = new List<string>();
            if (!string.IsNullOrEmpty(columnToAdd) && !ColumnConstants.Columns.ContainsKey(columnToAdd)) return false;
            if (!string.IsNullOrEmpty(columnToRemove) && !ColumnConstants.Columns.ContainsKey(columnToRemove)) return false;
            if (string.IsNullOrEmpty(columns) ) columnsTodisplay = screener.Columns;
            else
            {
                    var currentColumns = columns.Split(',').ToList();
                    if (currentColumns.Any(x => !ColumnConstants.Columns.ContainsKey(x))) return false;
                    columnsTodisplay.AddRange(currentColumns);
            }
            if (!string.IsNullOrEmpty(columnToAdd) && !columnsTodisplay.Contains(columnToAdd)) columnsTodisplay.Add(columnToAdd);
            if (!string.IsNullOrEmpty(columnToRemove) && columnsTodisplay.Contains(columnToRemove)) columnsTodisplay.Remove(columnToRemove);
            return true;
        }
    }
}