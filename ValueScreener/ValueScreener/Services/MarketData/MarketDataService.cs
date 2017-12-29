using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite.Internal;
using Newtonsoft.Json;
using ValueScreener.Models.Domain;

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

        public async Task<Dictionary<string, IMarketData>> GetMarketDataBatchStocksAsync(List<string> tickers)
        {
            if (!tickers.Any()) return new Dictionary<string, IMarketData>();
            var stockTickers = string.Join(",", tickers.ToArray());
            var url = $"{_serviceEntryPoint}market/batch?symbols={stockTickers.ToLower()}&types=quote";
            try
            {
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
                {
                    var resultStr = await hc.GetStringAsync(url);

                    var marketData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<String, IexMarketData>>>(resultStr);

                    var result = new Dictionary<string, IMarketData>();
                    foreach (var entry in marketData)
                    {
                        result.Add(entry.Key.ToLower(), entry.Value.Values.First());
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("url " + url, e);
            }
        }
        public async Task<Dictionary<string, IMarketData>> GetMarketDataBatchAsync(List<string> tickers)
        {
            var errMax = 2;
            var currErr = 0;
            while (currErr<errMax)
            {
                try
                {
                    return await GetMarketDataBatchStocksAsync(tickers);
                }
                catch (Exception)
                {
                    currErr++;
                }
            }
            return await GetMarketDataBatchStocksAsync(tickers);
        }
    }

    
}