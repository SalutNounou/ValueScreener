using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ValueScreener.Services.MarketData
{
    public class MarketDataService : IMarketDataService
    {
        private readonly string _serviceEntryPoint = "https://api.iextrading.com/1.0/stock/";

       

        public async Task<IMarketData> GetMarketDataAsync(string stockTicker)
        {
            try
            {
                var url = $"{_serviceEntryPoint}{stockTicker}/quote";
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
                {
                    var resultStr = await hc.GetStringAsync(url);
                    var marketData = JsonConvert.DeserializeObject<IexMarketData>(resultStr);
                    return marketData;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}