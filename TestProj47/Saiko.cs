using System.Globalization;
using System.Linq;
using System.Net;

namespace HSNXT
{
    /// <summary>
    /// Extension methods by NaamloosDT, from https://github.com/NaamloosDT/Saiko
    /// </summary>
    public static partial class Extensions
    {        
        public static string TryRemove(this string input, int index)
        {
            return input.Length > index ? input.Remove(index) : input;
        }

        public static bool IsImageUrl(this string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "HEAD";
            using (var resp = req.GetResponse())
            {
                return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                    .StartsWith("image/");
            }
        }

        public static bool EndsWith(this string i, params string[] matches) => matches.All(i.EndsWith);

        public static bool StartsWith(this string i, params string[] matches) => matches.All(i.StartsWith);

        public static bool EndsWithAny(this string i, params string[] matches) => matches.Any(i.EndsWith);

        public static bool StartsWithAny(this string i, params string[] matches) => matches.Any(i.StartsWith);

        public static bool EqualsAny(this string i, params string[] matches) => matches.Any(m => i == m);

        public static bool EqualsAll(this string i, params string[] matches) => matches.All(m => i == m);
    }
}