using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
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
