using Kraken.Client;
using Kraken.Client.DataTypes;
using System;
using System.Collections.Generic;

namespace Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new KrakenClient();

            var assets = client.GetAssetInfo().Result;
            var assetDictionary = BuildAssetDictionary(assets);

            var assetPairs = client.GetAssetPairs(assetDictionary).Result;

            foreach(var pair in assetPairs)
            {
                Console.WriteLine(pair.Name);
                Console.WriteLine($"--> {pair.BaseCurrency.CurrencyName}");
                Console.WriteLine($"--> {pair.QuoteCurrency.CurrencyName}");
            }

            Console.ReadKey();
        }

        private static Dictionary<String, Asset> BuildAssetDictionary(Asset[] assets)
        {
            var assetDict = new Dictionary<String, Asset>();

            foreach (var asset in assets)
            {
                assetDict.Add(asset.AssetName, asset);
            }

            return assetDict;
        }
    }
}