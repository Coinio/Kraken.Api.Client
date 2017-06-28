using Kraken.Client.DataTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kraken.Client.JsonConverters
{
    public class KrakenGetAssetsJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(KrakenAsset[]))
                return true;

            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This will never be called as CanWrite is set to false");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var errorJson = jsonObject["error"];
            var resultJson = jsonObject["result"];

            if (errorJson.HasValues)
                throw new HttpRequestException(errorJson.ToString());

            var assets = new List<KrakenAsset>();

            if (!resultJson.HasValues)
                return assets;

            foreach (var assetToken in resultJson.Children().Values())
            {
                if (!assetToken.HasValues)
                    continue;

                var parent = assetToken.Parent as JProperty;

                var asset = new KrakenAsset()
                {
                    AssetName = parent.Name,
                    Class = GetAssetClassFromToken(assetToken),
                    CurrencyName = GetCurrencyNameFromToken(assetToken),
                    Decimals = GetDecimalsFromToken(assetToken),
                    DisplayDecimals = GetDisplayDecimalsFromToken(assetToken)
                };

                assets.Add(asset);
            }

            return assets.ToArray();
        }

        private String GetCurrencyNameFromToken(JToken token)
        {
            return token["altname"]?.ToString();
        }

        private int GetDecimalsFromToken(JToken token)
        {
            var decimalsString = token["decimals"]?.ToString();

            return int.Parse(decimalsString);
        }

        private int GetDisplayDecimalsFromToken(JToken token)
        {
            var displayDecimalsString = token["display_decimals"]?.ToString();

            return int.Parse(displayDecimalsString);
        }

        private AssetClass GetAssetClassFromToken(JToken token)
        {
            AssetClass assetClass;

            var altClassString = token["aclass"]?.ToString();

            if(altClassString == null)
                throw new FormatException("aclass is empty");

            if (!Enum.TryParse(altClassString, ignoreCase: true, result: out assetClass))
                throw new FormatException("aclass is invalid");

            return assetClass;         
        }
    }
}
