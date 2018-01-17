using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DSharpPlus.Entities;

namespace DSPEmojiGenerator2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // ReSharper disable once PossibleNullReferenceException
            var dict = (IReadOnlyDictionary<string, string>) typeof(DiscordEmoji).GetProperty("UnicodeEmojis",
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .GetValue(null, null);
            var ar = dict.Select(kv => $@"public const string {
                    Clean(Regex.Replace(Regex.Replace(kv.Key, "[^a-zA-Z0-9]", "_"), "_(.)",
                        m => m.Groups[1].ToString().ToUpperInvariant()))
                } = ""{Cv(kv.Value)}"",");
            File.WriteAllLines("./emoji.txt", ar);
        }

        public static string Cv(string originalString)
        {
            var bytes = Encoding.UTF32.GetBytes(originalString);
            var asAscii = new StringBuilder();
            for (var idx = 0; idx < bytes.Length; idx += 4)
            {
                var codepoint = BitConverter.ToUInt32(bytes, idx);
                if (codepoint <= sbyte.MaxValue)
                    asAscii.Append(Convert.ToChar(codepoint));
                else if (codepoint <= ushort.MaxValue)
                    asAscii.AppendFormat(@"\u{0:x4}", codepoint);
                else
                    asAscii.AppendFormat(@"\U{0:x8}", codepoint);
            }

            return asAscii.ToString();
        }

        private static string Clean(string s)
        {
            return (char.ToUpper(s[0]) + s.Substring(1)).Replace("_", "");
        }
    }
}