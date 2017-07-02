using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    /// <summary>
    /// The parsed result of the 'https://api.kraken.com/0/public/AssetPairs' end point
    /// </summary>
    public class KrakenAssetPair
    {
        /// <summary>
        /// The name of the asset pair
        /// </summary>
        public String PairName { get; set; }
        /// <summary>
        /// The name of the base currency for the asset pair
        /// </summary>
        public String BaseName { get; set; }
        /// <summary>
        /// The name of the quote currency for the asset pair
        /// </summary>
        public String QuoteName { get; set; }
    }
}
