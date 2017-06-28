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

        private String _krakenBaseAddress;
        private String _krakenApiVersion = "0";

        public KrakenClient(String baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);          
        }

        public async Task<KrakenAsset[]> GetAssetInfo()
        {
            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/Assets";

            var response = await _httpClient.GetStringAsync(url);

            var assets = JsonConvert.DeserializeObject<KrakenAsset[]>(response, new KrakenGetAssetsJsonConverter());

            return assets;
        }

        public async Task<KrakenAssetPair[]> GetAssetPairs()
        {
            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/AssetPairs";

            var response = await _httpClient.GetStringAsync(url);

            var assetPairs = JsonConvert.DeserializeObject<KrakenAssetPair[]>(response, new KrakenGetAssetPairsJsonConverter());

            return assetPairs;
        }          

        public async Task<KrakenAssetPairTicker[]> GetAssetPairTickers(KrakenAssetPair[] assetPairs)
        {
            var pairNames = assetPairs.Select(p => p.PairName).ToArray();

            return await GetAssetPairTickers(pairNames);
        }

        public async Task<KrakenAssetPairTicker[]> GetAssetPairTickers(String[] pairNames)
        {
            var parameters = BuildParametersFromArray("pair", pairNames);

            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/Ticker?{parameters}";
            var response = await _httpClient.GetStringAsync(url);

            var tickers = JsonConvert.DeserializeObject<KrakenAssetPairTicker[]>(response, new KrakenGetTickerJsonConverter());

            return tickers;
        }

        private String BuildParametersFromArray(String parameterName, String[] values)
        {
            var builder = new StringBuilder();

            builder.Append("pair=");

            for(int index = 0; index < values.Length; index++)
            {
                builder.Append(values[index]);

                if (index < values.Length - 1)
                    builder.Append(",");
            }

            return builder.ToString();
        }
    }
}
