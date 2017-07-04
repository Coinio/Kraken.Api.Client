namespace Kraken.Client.DataTypes
{
    /// <summary>
    /// The time intervals accepted by the 'https://api.kraken.com/0/public/OHLC' end point
    /// </summary>
    public enum OHLCTimeInterval
    {
        OneMinute = 1,
        FiveMinutes = 5,
        FifteenMinutes = 15,
        ThirtyMinutes = 30,
        OneHour = 60,
        FourHours = 240,
        TwentyFourHours = 1440,
        OneWeek = 10080,
        Fortnight = 21600
    }
}
