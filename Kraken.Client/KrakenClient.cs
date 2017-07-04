using Kraken.Client.DataTypes;
using Kraken.Client.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.Client
{
    public class KrakenClient
    {
        private HttpClient _httpClient;
        private String _krakenApiVersion = "0";            

        public KrakenClient(String baseAddress)
        {
            _httpClient = new HttpClient();                     
            _httpClient.BaseAddress = new Uri($"{baseAddress}");          
        }

        /// <summary>
        /// URL: https://api.kraken.com/0/public/Assets
        /// </summary>
        /// <returns></returns>
        public async Task<KrakenAsset[]> GetAssetInfo()
        {
            var url = $"/{_krakenApiVersion}/public/Assets";

            var response = await _httpClient.GetStringAsync(url);

            var assets = JsonConvert.DeserializeObject<KrakenAsset[]>(response, new KrakenGetAssetsJsonConverter());

            return assets;
        }

        /// <summary>
        /// URL: https://api.kraken.com/0/public/AssetPairs
        /// </summary>
        /// <returns></returns>
        public async Task<KrakenAssetPair[]> GetAssetPairs()
        {
            var url = $"/{_krakenApiVersion}/public/AssetPairs";

            var response = await _httpClient.GetStringAsync(url);

            var assetPairs = JsonConvert.DeserializeObject<KrakenAssetPair[]>(response, new KrakenGetAssetPairsJsonConverter());

            return assetPairs;
        }

        /// <summary>
        /// URL: https://api.kraken.com/0/public/Ticker
        /// </summary>
        /// <param name="assetPairs"></param>
        /// <returns></returns>
        public async Task<KrakenAssetPairTicker[]> GetAssetPairTickers(KrakenAssetPair[] assetPairs)
        {
            var pairNames = assetPairs.Select(p => p.PairName).ToArray();

            return await GetAssetPairTickers(pairNames);
        }

        /// <summary>
        /// URL: https://api.kraken.com/0/public/Ticker
        /// </summary>
        /// <param name="pairNames"></param>
        /// <returns></returns>
        public async Task<KrakenAssetPairTicker[]> GetAssetPairTickers(String[] pairNames)
        {
            var parameters = BuildParameterArray("pair", pairNames);

            var url = $"/{_krakenApiVersion}/public/Ticker?{parameters}";
            var response = await _httpClient.GetStringAsync(url);

            var tickers = JsonConvert.DeserializeObject<KrakenAssetPairTicker[]>(response, new KrakenGetTickerJsonConverter());

            return tickers;
        }

        /// <summary>
        /// URL: https://api.kraken.com/0/public/OHLC
        /// </summary>
        /// <param name="assetPairName">The name of the asset pair to query. The kraken api only accepts a single asset pair name for this end point.</param>
        /// <param name="interval">The interval of the OHLC data.</param>
        /// <param name="lastPollingId">The last polling id to retrieve results from. Can be used to limit the results requested to only those after the last GET request. </param>
        /// <returns></returns>
        public async Task<KrakenOHLCData> GetOHLC(String assetPairName, OHLCTimeInterval interval, long? lastPollingId = null)
        {
            var pairParams = BuildParameter("pair", assetPairName);
            var intervalParam = BuildParameter("interval", (int)interval);
            var lastPollingIdParam = BuildParameter("since", lastPollingId);

            var url = $"/{_krakenApiVersion}/public/OHLC?{pairParams}&{intervalParam}&{lastPollingIdParam}";

            var response = await _httpClient.GetStringAsync(url);

            var ohlcData = JsonConvert.DeserializeObject<KrakenOHLCData>(response, new KrakenGetOHLCJsonConverter());

            return ohlcData;
        }

        private String BuildParameter(String parameterName, object obj)
        {
            if (obj == null)
                return String.Empty;

            return $"{parameterName}={obj}";
        }

        private String BuildParameterArray(String parameterName, object[] values)
        {
            var builder = new StringBuilder();

            builder.Append($"{parameterName}=");

            for(int index = 0; index < values.Length; index++)
            {
                builder.Append(values[index].ToString());

                if (index < values.Length - 1)
                    builder.Append(",");
            }

            return builder.ToString();
        }
    }
}
