using System;

namespace Kraken.Client.DataTypes
{
    /// <summary>
    /// The container for the array of OHLC entries returned by the 'https://api.kraken.com/0/public/OHLC' end point
    /// </summary>
    public class KrakenOHLCEntry
    {
        /// <summary>
        /// The time for the OHLC row
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// The open price for the row
        /// </summary>
        public Decimal OpenPrice { get; set; }
        /// <summary>
        /// The high price for the row
        /// </summary>
        public Decimal HighPrice { get; set; }
        /// <summary>
        /// The low price for the row
        /// </summary>
        public Decimal LowPrice { get; set; }
        /// <summary>
        /// The close price for the row
        /// </summary>
        public Decimal ClosePrice { get; set; }
        /// <summary>
        /// The volume weighted average price for the row
        /// </summary>
        public Decimal VolumeWeightedAveragePrice { get; set; }
        /// <summary>
        /// The volume of shares traded for this entry
        /// </summary>
        public Decimal Volume { get; set; }
        /// <summary>
        /// The number of trades for this entry
        /// </summary>
        public int TradeCount { get; set; }
    }
}
