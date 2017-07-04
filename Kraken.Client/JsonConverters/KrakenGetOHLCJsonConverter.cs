using Kraken.Client.DataTypes;
using Kraken.Client.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Kraken.Client.JsonConverters
{
    /// <summary>
    /// Convert the Json response from the 'https://api.kraken.com/0/public/OHLC' end point
    /// </summary>
    internal class KrakenGetOHLCJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(KrakenOHLCData))
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

            var ohlcJson = resultJson.Children().First() as JProperty;
            var lastJson = resultJson.Children().Last() as JProperty;

            var ohlcData = new KrakenOHLCData()
            {
                PairName = ohlcJson.Name,
                LastPollingId = GetPollingIdFromToken(lastJson)
            };

            var ohlcEntries = new List<KrakenOHLCEntry>();

            foreach (var ohlcEntry in ohlcJson.Children().Values())
            {
                if (!ohlcEntry.HasValues)
                    continue;

                var entry = new KrakenOHLCEntry()
                {
                    Time = GetDateTimeFromToken(ohlcEntry[0]),
                    OpenPrice = ohlcEntry[1].ToObject<Decimal>(), 
                    HighPrice = ohlcEntry[2].ToObject<Decimal>(),
                    LowPrice = ohlcEntry[3].ToObject<Decimal>(),
                    ClosePrice = ohlcEntry[4].ToObject<Decimal>(),
                    VolumeWeightedAveragePrice = ohlcEntry[5].ToObject<Decimal>(),
                    Volume = ohlcEntry[6].ToObject<Decimal>(),
                    TradeCount = ohlcEntry[7].ToObject<int>()
                };

                ohlcEntries.Add(entry);
            }

            ohlcData.Entries = ohlcEntries.ToArray();

            return ohlcData;
        }

        private DateTime GetDateTimeFromToken(JToken token)
        {
            var unixtime = long.Parse(token.ToString());

            return unixtime.ToDateTime();
        }

        private long GetPollingIdFromToken(JToken token)
        {
            return long.Parse(token.First.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This will never be called as CanWrite will always return false");
        }
    }
}