using System;
using System.Collections.Generic;
using System.Text;

namespace Kraken.Client.DataTypes
{
    public enum AssetClass
    {
        Currency = 1
    }

    /// <summary>
    /// The parsed result of the 'https://api.kraken.com/0/public/Assets' end point
    /// </summary>
    public class KrakenAsset
    {     
        /// <summary>
        /// The asset class (only Currency for now)
        /// </summary>
        public AssetClass Class { get; set; }
        /// <summary>
        /// The name of the asset as defined by the Kraken Api
        /// </summary>
        public String AssetName { get; set; }
        /// <summary>
        /// The name of the actual currency
        /// </summary>
        public String CurrencyName { get; set; }
        /// <summary>
        /// The maximum decimal places for the currency
        /// </summary>
        public int Decimals { get; set; }
        /// <summary>
        /// The number of decimal places to display for the currency
        /// </summary>
        public int DisplayDecimals { get; set; }
    }
}
