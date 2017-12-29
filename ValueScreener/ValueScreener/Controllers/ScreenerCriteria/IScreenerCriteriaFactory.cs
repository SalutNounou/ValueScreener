using System;
using System.Collections.Generic;
using ValueScreener.Controllers.ScreenerColumns;

namespace ValueScreener.Controllers.ScreenerCriteria
{
    public interface IScreenerCriteriaFactory
    {
        IScreenerCriteria GetCriteria(string id);
        bool CriteriaExists(string id);
    }

    public class ScreenerCriteriaFactory : IScreenerCriteriaFactory
    {
        private readonly Dictionary<string, IScreenerCriteria> _criterias = new Dictionary<string, IScreenerCriteria>
        {
            {ColumnConstants.NcavDiscount, new NcavDiscountScreenerCriteria() },
            {ColumnConstants.MarketCap, new MarketCapScreenerCriteria()},
            {ColumnConstants.Price, new MarketCapScreenerCriteria()},
            {ColumnConstants.Ncav, new NcavScreenerCriteria()},
            {ColumnConstants.Country, new CountryCriteria() },
            {ColumnConstants.Currency, new CurrencyCriteria() },
            {ColumnConstants.Industry, new IndustryCriteria() },
            {ColumnConstants.Sector, new SectorCriteria()},
            {ColumnConstants.Market, new MarketCriteria()},

            {ColumnConstants.Piotroski, new PiotroskiScreenerCriteria()},
            {ColumnConstants.PiotroskiAvg, new AvgPiotroskiScreenerCriteria()},

            {ColumnConstants.Per, new PeScreenerCriteria()},
            {ColumnConstants.PbRatio, new PbScreenerCriteria()},
            {ColumnConstants.PriceToSales, new PsScreenerCriteria()},
            {ColumnConstants.EnterpriseMultiple, new EnterpriseMultipleScreenerCriteria()},
            {ColumnConstants.PriceToFreeCashFlow, new PFcfScreenerCriteria()},
            {ColumnConstants.AvgPriceToFreeCashFlow, new AvgPFcfScreenerCriteria()},

            {ColumnConstants.Roa, new RoaScreenerCriteria()},
            {ColumnConstants.AvgRoa, new AvgRoaScreenerCriteria()},
            {ColumnConstants.Roe, new RoeScreenerCriteria()},
            {ColumnConstants.AvgRoe, new AvgRoeScreenerCriteria()},
            {ColumnConstants.Roic, new RoicScreenerCriteria()},
            {ColumnConstants.RoicAvg, new AvgRoicScreenerCriteria()},

            {ColumnConstants.SolvencyRatio, new SolvencyScreenerCriteria()},
            {ColumnConstants.InterestCovered, new InterestCoveredScreenerCriteria()},
            {ColumnConstants.Leverage, new LeverageScreenerCriteria()},
            {ColumnConstants.AvgLeverage, new AvgLeverageScreenerCriteria()},

            {ColumnConstants.SalesGrowth, new SalesGrowthScreenerCriteria()},
            {ColumnConstants.AvgSalesGrowth, new AvgSalesGrowthScreenerCriteria()},
            {ColumnConstants.GrossMargin, new GrossMarginScreenerCriteria()},
            {ColumnConstants.AvgGrossMargin, new AvgGrossMArginScreenerCriteria()},
            {ColumnConstants.NetMargin, new NetMarginScreenerCriteria()},
            {ColumnConstants.AvgNetMargin, new AvgNetMarginScreenerCriteria()},

            {ColumnConstants.AssetTurnover, new AssetTurnoverMarginScreenerCriteria()},
            {ColumnConstants.AvgAssetTurnover, new AvgAssetTurnoverScreenerCriteria()},
            {ColumnConstants.CurrentRatio, new CurrentRatioScreenerCriteria()},
            {ColumnConstants.AvgCurrentRatio, new AvgCurrentRatioScreenerCriteria()},
            {ColumnConstants.QuickRatio, new QuickRatioScreenerCriteria()},
            {ColumnConstants.AvgQuickRatio, new AvgQuickRatioScreenerCriteria()},
        };
        public IScreenerCriteria GetCriteria(string id)
        {
                if (_criterias.ContainsKey(id)) return _criterias[id];
                return null;
        }

        public bool CriteriaExists(string id)
        {
            return _criterias.ContainsKey(id);
        }
    }
}