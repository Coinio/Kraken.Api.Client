using System;

namespace Kraken.Client.DataTypes
{
    /// <summary>
    /// The result type of the 'https://api.kraken.com/0/public/OHLC' end point
    /// </summary>
    public class KrakenOHLCData
    {
        /// <summary>
        /// The asset pair name for the OHLC data
        /// </summary>
        public String PairName { get; set; }
        /// <summary>
        /// The array of OHLC data entries for the result
        /// </summary>
        public KrakenOHLCEntry[] Entries { get; set;}
        /// <summary>
        /// This Id is used by the 'since' parameter of the OHLC end point to continue reading data from the last OHLC reading.
        /// </summary>
        public long LastPollingId { get; set; }
    }
}
