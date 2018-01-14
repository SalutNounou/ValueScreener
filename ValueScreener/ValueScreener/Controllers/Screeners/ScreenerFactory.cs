using System.Collections.Generic;

namespace ValueScreener.Controllers.Screeners
{
    public class ScreenerFactory : IScreenerFactory
    {
        private readonly Dictionary<string, IScreener> _screeners = new Dictionary<string, IScreener>
        {
            { "pricetosales", new PriceToSalesScreener()},
            {"netnet", new NetNetScreener() }
        };
        public IScreener GetScreener(string criteria)
        {
            if (_screeners.ContainsKey(criteria)) return _screeners[criteria];
            return null;
        }
    }
}