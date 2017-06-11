using Kraken.Client;
using Kraken.Client.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new KrakenClient();

            var assets = client.GetAssetInfo().Result;
            var assetCache = BuildAssetCache(assets);

            var assetPairs = client.GetAssetPairs(assetCache).Result;

            var tickers = client.GetAssetPairTickers(assetPairs).Result;

            foreach(var ticker in tickers)
            {
                Console.WriteLine($"{ticker.AssetPair.Name}     {ticker.AssetPair.BaseCurrency.CurrencyName} -> {ticker.AssetPair.QuoteCurrency.CurrencyName}");
                Console.WriteLine($"-> Ask: {ticker.AskPrice} Bid: {ticker.BidPrice} Last: {ticker.LastPrice}");
            }

            Console.ReadKey();
        }

        private static Dictionary<String, Asset> BuildAssetCache(Asset[] assets)
        {
            var assetDict = new Dictionary<String, Asset>();

            foreach (var asset in assets)
                assetDict.Add(asset.AssetName, asset);

            return assetDict;
        }

        private static Dictionary<String, AssetPair> BuildAssetPairCache(AssetPair[] assetPairs)
        {
            var assetPairDict = new Dictionary<String, AssetPair>();

            foreach (var assetPair in assetPairs)
                assetPairDict.Add(assetPair.Name, assetPair);

            return assetPairDict;
        }
    }
}