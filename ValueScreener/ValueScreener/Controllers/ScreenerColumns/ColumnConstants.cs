using System;
using System.Collections.Generic;

namespace ValueScreener.Controllers.ScreenerColumns
{
    public class ColumnConstants
    {
        public const string Country = "country";
        public const string CountryDisplay = "Country";
        public const string Sector = "sector";
        public const string SectorDisplay = "Sector";
        public const string NcavDiscount = "ncavdiscount";
        public const string NcavDiscountDisplay = "Discount on NCAV";
        public const string PriceToSales = "pricetosales";
        public const string PriceToSalesDisplay = "Price To Sales Ratio";
        public const string CompanyName = "companyname";
        public const string CompanyNameDisplay = "Company Name";
        public const string Ticker = "ticker";
        public const string TickerDisplay = "Ticker";


        public const string Roa = "roa";
        public const string RoaDisplay = "ROA";
        public const string AvgRoa = "roaavg";
        public const string AvgRoaDisplay = "ROA (Avg)";
        public const string Roe = "roe";
        public const string RoeDisplay = "ROE";
        public const string AvgRoe = "roeavg";
        public const string AvgRoeDisplay = "ROE (Avg)";
        public const string Roic = "roic";
        public const string RoicDisplay = "ROIC";
        public const string RoicAvg = "roicavg";
        public const string AvgRoicDisplay = "ROIC (Avg)";
        public const string Piotroski = "piotroski";
        public const string PiotroskiDisplay = "Piotroski Score";
        public const string PiotroskiAvg = "piotroskiavg";
        public const string AvgPiotroskiDisplay = "Piotroski Score(Avg)";

        public const string Per = "per";
        public const string PerDisplay = "P/E ratio";
        public const string PerAveg = "peravg";
        public const string AvgPerDisplay = "P/E (Avg)";

        public const string EnterpriseMultiple = "enterprisemultiple";
        public const string EnterpriseMultipleDisplay = "Ev/Ebitda";

        public const string Industry = "industry";
        public const string IndustryDisplay = "Industry";

        public const string MarketCap = "marketcap";
        public const string MarketCapDisplay = "Market Capitalization";

        public const string Ncav = "ncav";
        public const string NcavDisplay = "Net Current Asset Value";


        public static Dictionary<string, string> Columns = new Dictionary<string, string>()
        {
            {Roa,RoaDisplay },
            {AvgRoa,AvgRoaDisplay },
            {Roe,RoeDisplay },
            {AvgRoe,AvgRoeDisplay },
            {Roic,RoicDisplay },
            {RoicAvg,AvgRoicDisplay },
            {Piotroski,PiotroskiDisplay },
            {PiotroskiAvg,AvgPiotroskiDisplay },
            {MarketCap,MarketCapDisplay },
            {Ncav,NcavDisplay },
            {NcavDiscount,NcavDiscountDisplay},
            {EnterpriseMultiple, EnterpriseMultipleDisplay },
            {Per, PerDisplay },
            {PriceToFreeCashFlow, PriceToFreeCashFlowDisplay },
            {AvgPriceToFreeCashFlow, AvgPriceToFreeCashFlowDisplay },
            {PriceToSales, PriceToSalesDisplay },
            {Currency, CurrencyDisplay },
            {Market, MarketDisplay },
            {Price, PriceDisplay },
            {SolvencyRatio, SolvencyRatioDisplay },
            {InterestCovered, InterestCoveredDisplay },
            {Leverage, LeverageDisplay },
            {AvgLeverage, AvgLeverageDisplay },

            {SalesGrowth, SalesGrowthDisplay },
            {AvgSalesGrowth, AvgSalesGrowthDisplay },
            {GrossMargin, GrossMarginDisplay },
            {AvgGrossMargin, AvgGrossMarginDisplay },
            {NetMargin, NetMarginDisplay },
            {AvgNetMargin, AvgNetMarginDisplay },
            {AssetTurnover, AssetTurnoverDisplay },
            {AvgAssetTurnover, AvgAssetTurnoverDisplay },
           
            {CurrentRatio, CurrencyDisplay },
            {AvgCurrentRatio, AvgCurrentRatioDisplay },
            {QuickRatio, QuickRatioDisplay },
            {AvgQuickRatio, AvgQuickRatioDisplay },

            {Country,CountryDisplay },
            {Sector,SectorDisplay },
            {Industry,IndustryDisplay },

        };

        public const string CurrencyDisplay = "Currency";
        public const string Currency = "currency";
        public const string MarketDisplay = "Market";
        public const string Market = "market";
        public const string Price = "price";
        public const string PriceDisplay = "Price";
        public const string SolvencyRatio = "solvency";
        public const string SolvencyRatioDisplay = "Solvency Ratio";

        public const string InterestCoveredDisplay = "Times Interest Covered";
        public const string InterestCovered = "interestcovered";

        public const string SalesGrowth = "salesGrowth";
        public const string SalesGrowthDisplay = "Sales Growth";
        public const string AvgSalesGrowth = "avgsalesgrowth";
        public const string AvgSalesGrowthDisplay = "Sales Growth (Avg)";

        public const string GrossMargin = "grossmargin";
        public const string GrossMarginDisplay = "Gross Margin";
        public const string AvgGrossMargin = "avggrossmargin";
        public const string AvgGrossMarginDisplay = "Gross Margin (Avg)";

        public const string NetMargin = "netmargin";
        public const string NetMarginDisplay = "Net Margin";
        public const string AvgNetMargin = "avgnetmargin";
        public const string AvgNetMarginDisplay = "Net Margin (Avg)";

        public const string AssetTurnover = "assetturnover";
        public const string AssetTurnoverDisplay = "Asset Turnover";
        public const string AvgAssetTurnover = "avgassetturnover";
        public const string AvgAssetTurnoverDisplay = "Asset Turnover (Avg)";

        public const string PriceToFreeCashFlow = "pfcf";
        public const string PriceToFreeCashFlowDisplay = "P/FCF";
        public const string AvgPriceToFreeCashFlow = "avgpfcf";
        public const string AvgPriceToFreeCashFlowDisplay = "P/FCF (Avg)";

        public const string Leverage = "leverage";
        public const string LeverageDisplay = "Leverage";
        public const string AvgLeverage = "avgleverage";
        public const string AvgLeverageDisplay = "Leverage (Avg)";

        public const string CurrentRatio = "currentratio";
        public const string CurrentRatioDisplay = "Current Ratio";
        public const string AvgCurrentRatio = "avgcurrentratio";
        public const string AvgCurrentRatioDisplay = "Current Ratio (Avg)";

        public const string QuickRatio = "quickratio";
        public const string QuickRatioDisplay = "Quick Ratio";
        public const string AvgQuickRatio = "avgquickratio";
        public const string AvgQuickRatioDisplay = "Quick Ratio (Avg)";



    }
}