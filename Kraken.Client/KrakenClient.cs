using Kraken.Client.DataTypes;
using Kraken.Client.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<AssetPair[]> GetAssetPairs(Dictionary<String, Asset> assets)
        {
            var url = $"{_krakenBaseAddress}/{_krakenApiVersion}/public/AssetPairs";

            var response = await _httpClient.GetStringAsync(url);

            var assetPairs = JsonConvert.DeserializeObject<AssetPair[]>(response, new KrakenGetAssetPairsJsonConverter(assets));

            return assetPairs;
        }
    }
}
