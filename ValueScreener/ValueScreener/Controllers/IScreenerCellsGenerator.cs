﻿using System.Collections.Generic;
using ValueScreener.Controllers.ScreenerColumns;
using ValueScreener.Models.Domain;
using ValueScreener.Models.ViewModels;

namespace ValueScreener.Controllers
{
    public interface IScreenerCellsGenerator
    {
        IEnumerable<ColumnTitle> GetColumnTitles(List<string> screenerColumns);
        IEnumerable<ScreenerRowViewModel> GetRows(IEnumerable<Stock> stocks, List<string> columns);
        IScreenerColumn GetColumn(string id);
    }

    public class ScreenerCellsGenerator : IScreenerCellsGenerator
    {
        private static readonly Dictionary<string, IScreenerColumn> Columns = new Dictionary<string, IScreenerColumn>
        {
            {ColumnConstants.CompanyName, new CompanyNameScreenerColumn() },
            {ColumnConstants.Ticker , new TickerScreenerColumn()},
            {ColumnConstants.NcavDiscount, new NcavDiscountScreenerColumn() },
            {ColumnConstants.Per, new PerScreenerColumn() },
            {ColumnConstants.PbRatio, new PriceBookScreenerColumn()},
            {ColumnConstants.PriceToSales, new PriceToSalesScreenerColumn()},
            {ColumnConstants.Country, new CountryScreenerColumn()},
            {ColumnConstants.Sector, new SectorScreenerColumn() },
            {ColumnConstants.Industry, new IndustryScreenerColumn() },
            {ColumnConstants.MarketCap, new MarketCapScreenerColumn() },

            {ColumnConstants.Ncav, new NcavScreenerColumn() },
            {ColumnConstants.Roa, new RoaScreenerColumn() },
            {ColumnConstants.AvgRoa, new AvgRoaScreenerColumn() },
            {ColumnConstants.Roe, new RoeScreenerColumn()},
            {ColumnConstants.AvgRoe,new AvgRoeScreenerColumn() },
            {ColumnConstants.Roic, new RoicScreenerColumn() },
            {ColumnConstants.RoicAvg, new AvgRoicScreenerColumn() },

            {ColumnConstants.Piotroski, new PiotroskiScreenerColumn() },
            {ColumnConstants.PiotroskiAvg, new AvgPiotroskiScreenerColumn() },

            {ColumnConstants.EnterpriseMultiple, new EnterpriseMultipleScreenerColumn() },
            
            {ColumnConstants.Currency, new CurrencyScreenerColumn() },
            {ColumnConstants.Market, new MarketScreenerColumn()},
            {ColumnConstants.Price, new PriceScreenerColumn()},
            {ColumnConstants.SolvencyRatio, new SolvencyScreenerColumn()},
            {ColumnConstants.InterestCovered, new InterestCoveredScreenerColumn()},

            {ColumnConstants.PriceToFreeCashFlow, new PFcfScreenerColumn()},
            {ColumnConstants.AvgPriceToFreeCashFlow, new AvgPFcfScreenerColumn()},
            {ColumnConstants.SalesGrowth, new SalesGrowthScreenerColumn()},
            {ColumnConstants.AvgSalesGrowth, new AvgSalesGrowthScreenerColumn()},

            {ColumnConstants.GrossMargin, new GrossMarginScreenerColumn()},
            {ColumnConstants.AvgGrossMargin, new AvgGrossMarginScreenerColumn()},
            {ColumnConstants.NetMargin, new NetMarginScreenerColumn()},
            {ColumnConstants.AvgNetMargin, new AvgNetMarginScreenerColumn()},

            {ColumnConstants.AssetTurnover, new AssetTurnOverScreenerColumn()},
            {ColumnConstants.AvgAssetTurnover, new AvgAssetTurnoverScreenerColumn()},
            {ColumnConstants.Leverage, new LeverageColumn()},
            {ColumnConstants.AvgLeverage, new AvgLeverageScreenerColumn()},

            {ColumnConstants.CurrentRatio, new CurrentRatioScreenerColumn()},
            {ColumnConstants.AvgCurrentRatio, new AvgCurrentRatioScreenerColumn()},
            {ColumnConstants.QuickRatio, new QuickRatioScreenerColumn()},
            {ColumnConstants.AvgQuickRatio, new AvgQuickRatioScreenerColumn()},
        };

        public IEnumerable<ColumnTitle> GetColumnTitles(List<string> screenerColumns)
        {
            var columnnames = new List<ColumnTitle> {
                    new ColumnTitle{IsSticky = true,Title = ColumnConstants.TickerDisplay, columnId = ColumnConstants.Ticker},
                    new ColumnTitle{IsSticky = true, Title = ColumnConstants.CompanyNameDisplay, columnId = ColumnConstants.CompanyName}
            };
            foreach (var column in screenerColumns)
            {
                if (Columns.ContainsKey(column))
                    columnnames.Add(new ColumnTitle{IsSticky = false,Title = Columns[column].DisplayName,columnId = column});
            }
            return columnnames;
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

        public IScreenerColumn GetColumn(string id)
        {
            if (Columns.ContainsKey(id))
                return Columns[id];
            return null;
        }
    }




}