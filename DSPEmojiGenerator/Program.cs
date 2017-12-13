using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DSPEmojiGenerator
{
    public class EmojiJson
    {
        [JsonProperty("char")]
        public string Char { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("no")]
        public long No { get; set; }
    }

    internal class Program
    {
        private static void Main()
        {
            var big1 = JsonConvert.DeserializeObject<EmojiJson[]>(Class1.Big1);

            var arr = big1.Select(e =>
            {
                e.Char = Dickscord.Program.Cv(e.Char);
                return e;
            }).Select(e => $@"public const string {
                    Clean(Regex.Replace(Regex.Replace(e.Name, "[^a-zA-Z0-9]", "_"), "_(.)",
                        m => m.Groups[1].ToString().ToUpperInvariant()))
                } = ""{e.Char}"";").ToArray();
            
            Console.WriteLine(string.Join("\n", arr));

            File.WriteAllLines("out.txt", arr);
        }

        private static string Clean(string s)
        {
            return (char.ToUpper(s[0]) + s.Substring(1)).Replace("_", "");
        }
    }
}