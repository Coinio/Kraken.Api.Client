using System;

namespace Kraken.Client.Extensions
{
    /// <summary>
    /// Extension methods related to parsing unixtime to DateTime
    /// </summary>
    public static class UnixTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToDateTime(this long unixtime)
        {            
            var offset = DateTimeOffset.FromUnixTimeSeconds(unixtime);

            return offset.DateTime;
        }

        public static long ToUnixTime(this DateTime dateTime)
        {
            return (long)(dateTime - Epoch).TotalSeconds;
        }
    }
}
