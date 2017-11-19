using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Replaces substring inside string where matched. Will return original if no match found.
        /// </summary>
        public static string Replace(this string source, string oldString, string newString, StringComparison comp)
        {
            var index = source.IndexOf(oldString, comp);
            var matchFound = index >= 0;

            if (!matchFound) return source;
            // Remove the old text.
            source = source.Remove(index, oldString.Length);

            // Add the new text.
            source = source.Insert(index, newString);

            return source;
        }
        
        /// <summary>
        /// Checks if the string matches all of the regex patterns.
        /// </summary>
        public static bool IsMatchAll(this string value, params string[] patterns)
        {
            return patterns.Select(pat => new Regex(pat)).All(regex => regex.IsMatch(value));
        }
        
        /// <summary>
        /// Checks if the string matches any of the regex patterns.
        /// </summary>
        public static bool IsMatchAny(this string value, params string[] patterns)
        {
            return patterns.Select(pat => new Regex(pat)).Any(regex => regex.IsMatch(value));
        }
    }
}
