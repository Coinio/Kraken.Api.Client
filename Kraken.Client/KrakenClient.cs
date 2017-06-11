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

        private String _krakenBaseAddress = "https://api.kraken.com";
        private String _krakenApiVersion = "0";

        public KrakenClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.kraken.com");          
        }

        public async Task<Asset[]> GetAssetInfo()
        {
            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/Assets";

            var response = await _httpClient.GetStringAsync(url);

            var assets = JsonConvert.DeserializeObject<Asset[]>(response, new KrakenGetAssetsJsonConverter());

            return assets;
        }

        public async Task<AssetPair[]> GetAssetPairs(Dictionary<String, Asset> assetCache)
        {
            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/AssetPairs";

            var response = await _httpClient.GetStringAsync(url);

            var assetPairs = JsonConvert.DeserializeObject<AssetPair[]>(response, new KrakenGetAssetPairsJsonConverter(assetCache));

            return assetPairs;
        }          

        public async Task<AssetPairTicker[]> GetAssetPairTickers(AssetPair[] assetPairs)
        {
            var parameters = BuildParametersFromArray("pair", assetPairs.Select(pair => pair.Name).ToArray());

            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/Ticker?{parameters}";

            var response = await _httpClient.GetStringAsync(url);

            var tickers = JsonConvert.DeserializeObject<AssetPairTicker[]>(response, new KrakenGetTickerJsonConverter(assetPairs));

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
