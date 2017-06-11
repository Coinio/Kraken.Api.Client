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
    /// A class to hold the details of an asset
    /// </summary>
    public class Asset
    {     
        /// <summary>
        /// Change to a strongly typed enumerable
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
