using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SuperOEISGenerator
{
    internal static class Program
    {
        private const string Header = @"using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SuperOEISGenerator.IO;

namespace OEISReader.DatabaseX
{";

        private const string Footer = @"
}";
        
        public static void Main()
        {
            var lines = File.ReadAllLines(@"C:\Users\Rafael\Downloads\oeis\over.txt")
                .Where(e => e.StartsWith("public static readonly "));

            var entries = new ConcurrentBag<string>();

            var enumerable = lines as string[] ?? lines.ToArray();
            Parallel.ForEach(enumerable, line =>
            {
                var type = line.Substring("public static readonly ".Length);
                type = type.Substring(0, type.IndexOf("[]", StringComparison.Ordinal));

                var oeisId = line.Substring(line.IndexOf("[] ", StringComparison.Ordinal) + 3);
                oeisId = oeisId.Substring(0, oeisId.IndexOf(' '));

                // { ... };
                var theArray = line.Substring(line.IndexOf(" = { ", StringComparison.Ordinal) + " = ".Length);

                //Console.WriteLine($"{oeisId}/{enumerable.Length}");
                if (type == "BigInteger")
                {
                    entries.Add($@"
    public static class {oeisId}
    {{
        public static readonly BigInteger[] Value = {theArray}
        public static readonly IEnumerable<byte[]> Bytes = Value.Select(e => e.ToByteArray());

        public static Stream Stream
        {{
            get
            {{
                var ms = new MemoryStream();
                for (var i = 0; i < Value.Length; i++)
                {{
                    var b = Value[i].ToByteArray();
                    ms.Write(b, 0, b.Length);
                }}
                return ms;
            }}
        }}
        
        public static Stream StreamLazy => new EnumerableStream(Bytes);
    }}

    public class {oeisId}Enumerable : IEnumerable<byte[]>
    {{
        public IEnumerator<byte[]> GetEnumerator()
        {{
            throw new NotImplementedException();
        }}

        IEnumerator IEnumerable.GetEnumerator()
        {{
            return GetEnumerator();
        }}
    }}

    public class {oeisId}Inst : IEnumerable<BigInteger>
    {{
        public static readonly BigInteger[] Value = {oeisId}.Value;

        public static Stream Stream
        {{
            get
            {{
                var ms = new MemoryStream();
                for (var i = 0; i < Value.Length; i++)
                {{
                    var b = Value[i].ToByteArray();
                    ms.Write(b, 0, b.Length);
                }}
                return ms;
            }}
        }}
        
        public static Stream StreamLazy => new EnumerableStream({oeisId}.Bytes);

        public BigInteger this[int i] => Value[i];
        
        public static {oeisId}Inst Instance = new {oeisId}Inst();
        
        public IEnumerator<BigInteger> GetEnumerator()
        {{
            return (Value as IEnumerable<BigInteger>).GetEnumerator();
        }}
        IEnumerator IEnumerable.GetEnumerator()
        {{
            return Value.GetEnumerator();
        }}
    }}");
                }
                else
                {
                    entries.Add($@"
    public static class {oeisId}
    {{
        public static readonly {type}[] Value = {theArray}
        public static readonly IEnumerable<byte[]> Bytes = Value.Select(BitConverter.GetBytes);

        public static Stream Stream
        {{
            get
            {{
                var ms = new MemoryStream();
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < Value.Length; i++)
                {{
                    var b = BitConverter.GetBytes(Value[i]);
                    ms.Write(b, 0, b.Length);
                }}
                return ms;
            }}
        }}
        
        public static Stream StreamLazy => new EnumerableStream(Bytes);
    }}

    public class {oeisId}Enumerable : IEnumerable<byte[]>
    {{
        public IEnumerator<byte[]> GetEnumerator()
        {{
            throw new NotImplementedException();
        }}

        IEnumerator IEnumerable.GetEnumerator()
        {{
            return GetEnumerator();
        }}
    }}

    public class {oeisId}Inst : IEnumerable<{type}>
    {{
        public static readonly {type}[] Value = {oeisId}.Value;

        public static Stream Stream
        {{
            get
            {{
                var ms = new MemoryStream();
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < Value.Length; i++)
                {{
                    var b = BitConverter.GetBytes(Value[i]);
                    ms.Write(b, 0, b.Length);
                }}
                return ms;
            }}
        }}
        
        public static Stream StreamLazy => new EnumerableStream({oeisId}.Bytes);

        public {type} this[int i] => Value[i];
        
        public static {oeisId}Inst Instance = new {oeisId}Inst();
        
        public IEnumerator<{type}> GetEnumerator()
        {{
            return (Value as IEnumerable<{type}>).GetEnumerator();
        }}
        IEnumerator IEnumerable.GetEnumerator()
        {{
            return Value.GetEnumerator();
        }}
    }}");
                }
            });

            var cnt = entries.Count;
            for (var i = 0; i < cnt; i += 100)
            {
                if (i %1000 == 0) Console.WriteLine("d"+i);
                var arr = new string[100];
                for (var j = 0; j < 100; j++)
                {
                    do
                    {
                        if (!entries.TryTake(out var got)) continue;
                        arr[j] = got;
                        break;
                    } while (true);
                }
                File.WriteAllText($@"C:\Users\Rafael\Downloads\oeis\Singular\OEIS{i.ToString().PadLeft(5)}.cs", 
                    Header + "\n" + string.Join("\n", arr) + "\n" + Footer);
            }
        }
    }
}