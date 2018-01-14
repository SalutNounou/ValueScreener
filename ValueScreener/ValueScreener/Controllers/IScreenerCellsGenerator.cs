using System.Collections.Generic;
using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers
{
    public interface IScreenerCellsGenerator
    {
        IEnumerable<string> GetColumnTitles(List<string> screenerColumns);
        IEnumerable<ScreenerRowViewModel> GetRows(IEnumerable<Stock> stocks, List<string> columns);
    }

    public class ScreenerCellsGenerator : IScreenerCellsGenerator
    {
        private static readonly  Dictionary<string, IScreenerColumn> Columns = new Dictionary<string, IScreenerColumn>
        {
            {ColumnConstants.CompanyName, new CompanyNameScreenerColumn() },
            {ColumnConstants.Ticker , new TickerScreenerColumn()},
            {ColumnConstants.NcavDiscount, new NcavDiscountScreenerColumn() },
            {ColumnConstants.PriceToSales, new PriceToSalesScreenerColumn()},
            {ColumnConstants.Country, new CountryScreenerColumn()},
            {ColumnConstants.Sector, new SectorScreenerColumn() },


            {ColumnConstants.Roa, new RoaScreenerColumn() },
            {ColumnConstants.AvgRoa, new AvgRoaScreenerColumn() },
            {ColumnConstants.Roe, new RoeScreenerColumn()},
            {ColumnConstants.AvgRoe,new AvgRoeScreenerColumn() },
            {ColumnConstants.Roic, new RoicScreenerColumn() },
            {ColumnConstants.RoicAvg, new AvgRoicScreenerColumn() },

            {ColumnConstants.Piotroski, new PiotroskiScreenerColumn() },
            {ColumnConstants.PiotroskiAvg, new AvgPiotroskiScreenerColumn() },

            {ColumnConstants.EnterpriseMultiple, new EnterpriseMultipleScreenerColumn() },
            {ColumnConstants.Per, new PerScreenerColumn() },
        };

        public IEnumerable<string> GetColumnTitles(List<string> screenerColumns)
        {
            var columnnames = new List<string>{ColumnConstants.Ticker, ColumnConstants.CompanyName};
            columnnames.AddRange(screenerColumns);
            foreach (var columnname in columnnames)
            {
                if (Columns.ContainsKey(columnname))
                    yield return Columns[columnname].DisplayName;
            }
        }

        public IEnumerable<ScreenerRowViewModel> GetRows(IEnumerable<Stock> stocks, List<string> columns)
        {
            var columnNames = new List<string> { ColumnConstants.Ticker, ColumnConstants.CompanyName };
            columnNames.AddRange(columns);
            var result = new List<ScreenerRowViewModel>();
            foreach (var stock in stocks)
            {
                var row = new ScreenerRowViewModel(stock.Id);
                foreach (var columnnName in columnNames)
                {
                    if (Columns.ContainsKey(columnnName))
                    {
                        row.Cells.Add(Columns[columnnName].GetCell(stock));
                    }
                }
                result.Add(row);
            }
            return result;
        }
    }




}