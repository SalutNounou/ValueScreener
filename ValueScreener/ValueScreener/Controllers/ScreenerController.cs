using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Controllers.ScreenerCriteria;
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
        private readonly IScreenerCriteriaFactory _criteriaFactory;

        public ScreenerController(ApplicationDbContext context, IScreenerFactory screenerFactory, IScreenerCellsGenerator cellsGenerator, IScreenerCriteriaFactory criteriaFactory)
        {
            _context = context;
            _screenerFactory = screenerFactory;
            _cellsGenerator = cellsGenerator;
            _criteriaFactory = criteriaFactory;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Screen(string criteria, int? page, string columns, string columnToAdd, string columnToRemove, string columnToken)
        {
            if (string.IsNullOrEmpty(criteria)) return NotFound();
            var screener = _screenerFactory.GetScreener(criteria);
            if (screener == null) return NotFound();
            if (!ManageColumns(screener, columns, columnToAdd, columnToRemove, out var columnsTodisplay, columnToken)) return NotFound();
            ViewData["Columns"] = string.Join(',', columnsTodisplay);
            ViewData["Criteria"] = criteria;
            ViewData["Title"] = screener.Name;

            var stocks = screener.LoadStocks(_context).Where(screener.SelectionCriteria);
            stocks = screener.Order(stocks);
            var pageSize = 25;

            var pageList = await PaginatedList<Stock>.CreateAsync(stocks.AsNoTracking(), page ?? 1, pageSize);

            var viewModel = new ScreenerViewModel(pageList.PageIndex, pageList.TotalPages, pageList.HasPreviousPage, pageList.HasNextPage);

            foreach (var column in ColumnConstants.Columns.Where(x => !columnsTodisplay.Contains(x.Key)))
            {
                viewModel.AvailableAdditionalColumns.Add(column.Key, column.Value);
            }

            viewModel.ColumnTitles.AddRange(_cellsGenerator.GetColumnTitles(columnsTodisplay));
            viewModel.Rows.AddRange(_cellsGenerator.GetRows(pageList, columnsTodisplay));
            return View(viewModel);
        }





        [AllowAnonymous]
        public async Task<IActionResult> ScreenGeneric(GenericScreenerViewModel viewModel)
        {
            GenericScreenerViewModel newViewModel;
            var stocks = await _context.Stocks
                .Include(s => s.MarketData)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.PiotroskiResults)
                .Include(s => s.PricingResult)
                .ThenInclude(p => p.AnnualResults)
                .Where(x => x.PricingResult != null && x.MarketData != null).AsNoTracking().ToListAsync();
               
            if (viewModel != null)
            {

                newViewModel = viewModel;
                if (newViewModel.Criterias == null) newViewModel.Criterias = new List<ScreenerCriteriaViewModel>();
                if (!string.IsNullOrEmpty(viewModel.CriteriaToAdd) && _criteriaFactory.CriteriaExists(viewModel.CriteriaToAdd))
                {
                    var criteria = _criteriaFactory.GetCriteria(viewModel.CriteriaToAdd);
                    var criteriaViewModel = new ScreenerCriteriaViewModel
                    {
                        Name = criteria.DisplayName,
                        Id = criteria.Id,
                        Operation = criteria.DefaultOperator,
                        ValueType = criteria.ValueType
                    };
                    newViewModel.Criterias.Add(criteriaViewModel);
                }
            }
            else
            {
                newViewModel = new GenericScreenerViewModel { Criterias = new List<ScreenerCriteriaViewModel>() };
            }

            foreach (var criteriaViewModel in newViewModel.Criterias)
            {
                var criteria = _criteriaFactory.GetCriteria(criteriaViewModel.Id);
                if (criteria == null) continue;
                stocks =  stocks
                    .Where(x => criteria.StockMatch(x,criteriaViewModel)).ToList(); 
            }

            var pageList =  PaginatedList<Stock>.Create(stocks, 1, 25);

            var columnsTodisplay = new List<string> {ColumnConstants.NcavDiscount,ColumnConstants.MarketCap, ColumnConstants.Ncav };
            newViewModel.ColumnTitles.AddRange(_cellsGenerator.GetColumnTitles(columnsTodisplay));
            newViewModel.Rows.AddRange(_cellsGenerator.GetRows(pageList, columnsTodisplay));

            return View(newViewModel);
        }

        private bool ManageColumns(IScreener screener, string columns, string columnToAdd, string columnToRemove, out List<string> columnsTodisplay, string columnToken)
        {
            columnsTodisplay = new List<string>();
            if (!string.IsNullOrEmpty(columnToAdd) && !ColumnConstants.Columns.ContainsKey(columnToAdd)) return false;
            if (!string.IsNullOrEmpty(columnToRemove) && !ColumnConstants.Columns.ContainsKey(columnToRemove)) return false;
            if (string.IsNullOrEmpty(columns) && string.IsNullOrEmpty(columnToken)) columnsTodisplay = screener.Columns;
            else
            {
                if (!string.IsNullOrEmpty(columns))
                {
                    var currentColumns = columns.Split(',').ToList();
                    if (currentColumns.Any(x => !ColumnConstants.Columns.ContainsKey(x))) return false;
                    columnsTodisplay.AddRange(currentColumns);
                }
            }
            if (!string.IsNullOrEmpty(columnToAdd) && !columnsTodisplay.Contains(columnToAdd)) columnsTodisplay.Add(columnToAdd);
            if (!string.IsNullOrEmpty(columnToRemove) && columnsTodisplay.Contains(columnToRemove)) columnsTodisplay.Remove(columnToRemove);
            return true;
        }
    }
}