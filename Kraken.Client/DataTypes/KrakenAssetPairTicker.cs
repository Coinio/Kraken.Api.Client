using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    /// <summary>
    /// The parsed result of the 'https://api.kraken.com/0/public/Ticker' end point
    /// </summary>
    public class KrakenAssetPairTicker
    {
        /// <summary>
        /// The asset pair name for the ticker information
        /// </summary>
        public String PairName { get; set; }
        /// <summary>
        /// The current ask price given in the quote currency of the asset pair
        /// </summary>
        public decimal AskPrice { get; set; }
        /// <summary>
        /// The current bid price given in the quote currency of the asset pair
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// The last price given in the quote currency of the asset pair
        /// </summary>
        public decimal LastPrice { get; set; }
    }
}
