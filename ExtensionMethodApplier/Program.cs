using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ExtensionMethodApplier
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string root = @"C:\Users\Rafael\Documents\GitHub\TestProject\ExtensionMethodHarvester\jsons";
            var entries = Directory.EnumerateFiles(root).Select(file => JsonConvert.DeserializeObject<Entry>(File.ReadAllText(file))).ToList();

            const string output = @"C:\Users\Rafael\Documents\GitHub\TestProject\ExtensionMethodApplier\";
            //
            var entries2 = entries
                .Select(e => $@"
/*
 * {e.Name}
 * {e.Description}
 * 
 * Author: {e.Author}
 * Submitted on: {e.SubmittedOn}
 * 
 * {Wrap(e.Entries)}
 */

{e.Entries[0]}
");
            var i = 0;
            foreach (var list in entries2.ToList().Split(100))
            {
                i++;
                File.WriteAllText($"{output}out{i}.cs", string.Join("\n\n", list));
            }
        }
        
        public static IEnumerable<List<T>> Split<T>(this List<T> locations, int nSize = 30)  
        {        
            for (var i=0; i < locations.Count; i+= nSize) 
            { 
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i)); 
            }  
        } 
        
        private static string Wrap(string[] a)
        {
            switch (a.Length)
            {
                case 1:
                    return "";
                case 2:
                    return "Example: \n * " + a[1];
                default:
                    return "Examples: \n * " + string.Join("\n", a.Skip(1));
            }
        }
    }
    
    internal class Entry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string SubmittedOn { get; set; }
        public string[] Entries { get; set; } = new string[0];
    }
}