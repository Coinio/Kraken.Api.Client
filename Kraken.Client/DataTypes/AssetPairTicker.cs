using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    public class AssetPairTicker
    {
        /// <summary>
        /// The asset pair for the ticker information
        /// </summary>
        public AssetPair AssetPair { get; set; }
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
