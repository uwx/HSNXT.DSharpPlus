using System;
using System.Collections.Generic;
using System.IO;
using HSNXT;

namespace OEISReader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ls = File.ReadAllLines(@"C:\Users\Rafael\Downloads\oeis\stripped");
            Console.WriteLine("read!");
            var outArr = new List<string>(ls.Length);
            outArr.PushSplit(@"using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace OEISReader
{
    internal class OEISDB
    {");
            
            Console.WriteLine("alloc!");
            for (var line = 0; line < ls.Length; line++)
            {
                if (line % 10000 == 0) Console.WriteLine(line);
                
                var l = ls[line];
                if (l.Length == 0) continue;
                if (l[0] == '#') continue;

                var bit1 = l.Substring(0, l.IndexOf(' '));
                var bit2 = l.Substring(bit1.Length + 1);
                if (bit2[0] == ',')
                {
                    bit2 = bit2.Substring(1);
                }
                if (bit2.LastChar() == ',')
                {
                    bit2 = bit2.ChopRight(1);
                }
                var split = bit2.Split(',');
                if (split.Length == 0 || split[0].Length == 0) continue;
                var ulongClean = true;
                var longClean = true;
                for (var i = 0; i < split.Length; i++)
                {
                    var s = split[i];
                    if (long.TryParse(s, out var lg2))
                    {
                        if (lg2 < 0) ulongClean = false;
                        split[i] = lg2 + "L"; //no interpolation here
                    }
                    else if (ulong.TryParse(s, out var lg))
                    {
                        longClean = false;
                        split[i] = lg + "UL"; //no interpolation here
                    }
                    else
                    {
                        longClean = false;
                        ulongClean = false;
                        split[i] = "BigInteger.Parse(\"" + s + "\")";
                    }
                    // future: maybe add Int128 and Int256
                    // https://github.com/everbytes/BigMath
                }
                var type = longClean ? "long[]" : ulongClean ? "ulong[]" : "BigInteger[]";
                outArr.Add("public static readonly " + type + " " + bit1 + " = { " + split.JoinBy(", ") + " };");
            }
            outArr.PushSplit(@"
    }
}");
            
            File.WriteAllLines(@"C:\Users\Rafael\Downloads\oeis\over.txt", outArr);
        }
    }
}