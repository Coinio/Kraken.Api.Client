using Kraken.Client;
using Kraken.Client.DataTypes;
using System;

namespace Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new KrakenClient("https://api.kraken.com");

            var assets = client.GetAssetInfo().Result;

            foreach (var asset in assets)
                Console.WriteLine($"{asset.AssetName} -> {asset.CurrencyName}");


            var assetPairs = client.GetAssetPairs().Result;

            foreach (var assetPair in assetPairs)
                Console.WriteLine($"{assetPair.PairName}: {assetPair.BaseName} -> {assetPair.QuoteName}");


            var tickers = client.GetAssetPairTickers(assetPairs).Result;

            foreach (var ticker in tickers)
                Console.WriteLine($"{ticker.PairName} -> Ask: {ticker.AskPrice} Bid: {ticker.BidPrice} Last: {ticker.LastPrice}");
            

            var ohlcData = client.GetOHLC("XETHZUSD", OHLCTimeInterval.FiveMinutes).Result;

            Console.WriteLine($"{ohlcData.PairName} -> Last Id: {ohlcData.LastPollingId}");

            foreach (var entry in ohlcData.Entries)
                Console.WriteLine($"({entry.Time}) Open: {entry.OpenPrice} / Close: {entry.ClosePrice} / High: {entry.HighPrice} / Low: {entry.LowPrice}");


            Console.ReadKey();
        }

    }
}