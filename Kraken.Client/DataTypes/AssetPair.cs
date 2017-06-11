using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    public class AssetPair
    {
        /// <summary>
        /// The name of the asset pair
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// The base currency for an asset pair
        /// </summary>
        public Asset BaseCurrency { get; set; }
        /// <summary>
        /// The quote currency for an asset pair
        /// </summary>
        public Asset QuoteCurrency { get; set; }
    }
}
