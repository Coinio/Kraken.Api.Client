using Kraken.Client.DataTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kraken.Client.JsonConverters
{
    /// <summary>
    /// Convert the Json response from the 'https://api.kraken.com/0/public/Ticker' end point
    /// </summary>
    internal class KrakenGetTickerJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(KrakenAssetPairTicker[]))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var errorJson = jsonObject["error"];
            var resultJson = jsonObject["result"];

            if (errorJson.HasValues)
                throw new HttpRequestException(errorJson.ToString());

            var assetPairTickers = new List<KrakenAssetPairTicker>();

            foreach (var tickerToken in resultJson.Children().Values())
            {
                if (!tickerToken.HasValues)
                    continue;

                var parent = tickerToken.Parent as JProperty;

                var ticker = new KrakenAssetPairTicker()
                {
                    PairName = parent.Name,
                    AskPrice = GetPriceFromToken(tickerToken["a"]),
                    BidPrice = GetPriceFromToken(tickerToken["b"]),
                    LastPrice = GetPriceFromToken(tickerToken["c"])
                };

                assetPairTickers.Add(ticker);
            }

            return assetPairTickers.ToArray();
        }

        private decimal GetPriceFromToken(JToken token)
        {
            var priceToken = token.First;

            return Decimal.Parse(token.First.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This will never be called as CanWrite will always return false");
        }
    }
}
