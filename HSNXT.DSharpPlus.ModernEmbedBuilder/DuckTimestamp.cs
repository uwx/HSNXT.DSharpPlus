using System;
using System.Globalization;

namespace DSharpPlus.ModernEmbedBuilder
{
    // TODO is this even right
    public struct DuckTimestamp
    {
        private static readonly string[] Formats = { 
            // Basic formats
            "yyyyMMddTHHmmsszzz",
            "yyyyMMddTHHmmsszz",
            "yyyyMMddTHHmmssZ",
            // Extended formats
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-ddTHH:mm:sszz",
            "yyyy-MM-ddTHH:mm:ssZ",
            // All of the above with reduced accuracy
            "yyyyMMddTHHmmzzz",
            "yyyyMMddTHHmmzz",
            "yyyyMMddTHHmmZ",
            "yyyy-MM-ddTHH:mmzzz",
            "yyyy-MM-ddTHH:mmzz",
            "yyyy-MM-ddTHH:mmZ",
            // Accuracy reduced to hours
            "yyyyMMddTHHzzz",
            "yyyyMMddTHHzz",
            "yyyyMMddTHHZ",
            "yyyy-MM-ddTHHzzz",
            "yyyy-MM-ddTHHzz",
            "yyyy-MM-ddTHHZ"
        };
        
        /// <summary>
        /// Implicitly converts DuckTimestamp to a DateTimeOffset.
        /// </summary>
        public static implicit operator DateTimeOffset?(DuckTimestamp c) => DateTimeOffset.FromUnixTimeMilliseconds(c.Value);

        /// <summary>
        /// Implicitly converts DateTimeOffset to a DuckTimestamp.
        /// </summary>
        public static implicit operator DuckTimestamp(DateTimeOffset? c) => new DuckTimestamp(c?.ToUnixTimeMilliseconds());

        /// <summary>
        /// Implicitly parse a DuckTimestamp from a string.
        /// </summary>
        public static implicit operator DuckTimestamp(string c) => (DateTimeOffset)ParseIso8601String(c);
        
        /// <summary>
        /// Implicitly parse a DuckTimestamp from a long millis.
        /// </summary>
        public static implicit operator DuckTimestamp(long millis) => DateTimeOffset.FromUnixTimeMilliseconds(millis);
        
        public static DuckTimestamp Now => new DuckTimestamp(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        
        public long Value { get; }

        private DuckTimestamp(long? seconds)
        {
            this.Value = seconds ?? 0;
        }

        public static DateTime ParseIso8601String(string str)
        {
            return DateTime.ParseExact(str, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}