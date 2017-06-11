﻿using Kraken.Client.DataTypes;
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
        private readonly Dictionary<String, Asset> _assets;

        public KrakenGetAssetPairsJsonConverter(Dictionary<String, Asset> assets)
        {
            _assets = assets ?? throw new ArgumentNullException("assets");
        }

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(AssetPair[]))
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

            var assetPairs = new List<AssetPair>();

            foreach (var assetPairToken in resultJson.Children().Values())
            {
                if (!assetPairToken.HasValues)
                    continue;

                var altname = assetPairToken["altname"]?.ToString();
                var baseName = assetPairToken["base"]?.ToString();
                var quoteName = assetPairToken["quote"]?.ToString();

                var assetPair = new AssetPair()
                {
                    Name = altname,
                    BaseCurrency = _assets[baseName],
                    QuoteCurrency = _assets[quoteName]
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