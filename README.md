# Kraken.Api.Client

Just playing about with this for a personal project and only a couple of GET end points exist so far. I'm intending to add support all of the available public / private with proper request throttling based on the rules in the documentation below.

The api documentation for the Kraken Api can be found here: https://www.kraken.com/help/api

Usage

Create a client:

```cs
  var client = new KrakenClient("https://api.kraken.com");
```

Request a list of available assets (https://api.kraken.com/0/public/Assets):

```cs
  var assets = await client.GetAssetInfo();

  foreach (var asset in assets)
    Console.WriteLine($"{asset.AssetName} -> {asset.CurrencyName}");
```

Request a list of available asset pairs (https://api.kraken.com/0/public/AssetPairs):

```cs
  var assetPairs = await client.GetAssetPairs();

  foreach (var assetPair in assetPairs)
    Console.WriteLine($"{assetPair.PairName}: {assetPair.BaseName} -> {assetPair.QuoteName}");
```
     
Request a list of asset pair tickers. This can be limited to a subset of asset pairs (https://api.kraken.com/0/public/Ticker):

```cs
  var tickers = await client.GetAssetPairTickers(assetPairs);

  foreach (var ticker in tickers)
    Console.WriteLine($"{ticker.PairName} -> Ask: {ticker.AskPrice} Bid: {ticker.BidPrice} Last: {ticker.LastPrice}");
```                
                
Request the OHLC (Open / High / Low / Closed) data. The interval for the data can specified and the 'since' parameter can be given as the lastPollingId (https://api.kraken.com/0/public/OHLC):

```cs
  var ohlcData = await client.GetOHLC("XETHZUSD", OHLCTimeInterval.FiveMinutes, lastPollingId);

  Console.WriteLine($"{ohlcData.PairName} -> Last Id: {ohlcData.LastPollingId}");

  foreach (var entry in ohlcData.Entries)
    Console.WriteLine($"({entry.Time}) Open: {entry.OpenPrice} / Close: {entry.ClosePrice} / High: {entry.HighPrice} / Low: {entry.LowPrice}");
```
