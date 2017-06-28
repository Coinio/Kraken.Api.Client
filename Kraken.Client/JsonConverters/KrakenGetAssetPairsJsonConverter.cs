using Kraken.Client.DataTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Kraken.Client.JsonConverters
{
    public class KrakenGetAssetPairsJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(KrakenAssetPair[]))
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

            var assetPairs = new List<KrakenAssetPair>();

            foreach (var assetPairToken in resultJson.Children().Values())
            {
                if (!assetPairToken.HasValues)
                    continue;
                
                var parent = assetPairToken.Parent as JProperty;

                var altname = parent?.Name;
                var baseName = assetPairToken["base"]?.ToString();
                var quoteName = assetPairToken["quote"]?.ToString();

                var assetPair = new KrakenAssetPair()
                {
                    PairName = altname,
                    BaseName = baseName,
                    QuoteName = quoteName
                };
                
                assetPairs.Add(assetPair);
            }

            return assetPairs.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This will never be called as CanWrite will always return false");
        }
    }
}
