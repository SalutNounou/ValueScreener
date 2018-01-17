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
        public const string PerDisplay = "PE ratio";
        public const string PerAveg = "peravg";
        public const string AvgPerDisplay = "PE (Avg)";

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
            {Roe,RoeDisplay },
            {Country,CountryDisplay },
            {Sector,SectorDisplay },
            {Industry,IndustryDisplay },
            {Roic,RoicDisplay },
            {AvgRoa,AvgRoaDisplay },
            {AvgRoe,AvgRoeDisplay },
            {RoicAvg,AvgRoicDisplay },
            {Piotroski,PiotroskiDisplay },
            {PiotroskiAvg,AvgPiotroskiDisplay },
            {MarketCap,MarketCapDisplay },
            {Ncav,NcavDisplay },
            {NcavDiscount,NcavDiscountDisplay},
            {EnterpriseMultiple, EnterpriseMultipleDisplay },
            {Per, PerDisplay },
            {PriceToSales, PriceToSalesDisplay },
        };

    }
}