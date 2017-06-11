using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    public class AssetPairTicker
    {
        /// <summary>
        /// The asset pair of the ticker information
        /// </summary>
        public AssetPair AssetPair { get; set; }
        /// <summary>
        /// The current ask price for base currency of the asset pair
        /// </summary>
        public decimal AskPrice { get; set; }
        /// <summary>
        /// The current bid price for the base currency of the asset pair
        /// </summary>
        public decimal BidPrice { get; set; }
    }
}
