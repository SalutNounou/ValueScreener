using System.Collections.Generic;

namespace ValueScreener.Controllers.Screeners
{
    public class ScreenerFactory : IScreenerFactory
    {
        private readonly Dictionary<string, IScreener> _screeners = new Dictionary<string, IScreener>
        {
            { ScreenerConstants.PriceToSales, new PriceToSalesScreener()},
            {ScreenerConstants.NetNets, new NetNetScreener() },
            {ScreenerConstants.Piotroski, new PiotroskiScreener()},
            {ScreenerConstants.AvgPiotroski, new AvgPiotroskiScreener()},
            {ScreenerConstants.EnterpriseMultiple, new EnterpriseMultipleScreener()},
            {ScreenerConstants.AvgRoic, new AvgRoicScreener()},
            {ScreenerConstants.Roic, new RoicScreener()},
            {ScreenerConstants.AvgRoe, new AvgRoeScreener()},
            {ScreenerConstants.Roe, new RoeScreener()},
            {ScreenerConstants.AvgRoa, new AvgRoaScreener()},
            {ScreenerConstants.Roa, new RoaScreener() }
        };
        public IScreener GetScreener(string criteria)
        {
            if (_screeners.ContainsKey(criteria)) return _screeners[criteria];
            return null;
        }
    }

    public class ScreenerConstants
    {
        public  const string NetNets = "netnet";
        public const string PriceToSales = "pricetosales";
        public const string Roa = "roa";
        public const string Piotroski = "piotroski";
        public const string EnterpriseMultiple = "enterpriseMultiple";
        public const string AvgRoic = "roicavg";
        public const string Roic = "roic";
        public const string AvgRoe = "roeavg";
        public const string Roe = "roe";
        public const string AvgRoa = "roaavg";
        public const string AvgPiotroski = "piotroskiavg";
    }
}