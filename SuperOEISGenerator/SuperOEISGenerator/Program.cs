using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperOEISGenerator
{
    internal static class Program
    {
        private static readonly Regex SpaceTrimmer = new Regex(" {2,}", RegexOptions.Compiled);
        private static readonly Regex StartLineTrimmer = new Regex("(^ +| +$|(,) +| +(=>) +| +(=) +|(\r\n)\r\n)", RegexOptions.Compiled | RegexOptions.Multiline);
        
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
            foreach (var b in {oeisId}.Bytes) {{
                yield return b;
            }}
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
            foreach (var b in {oeisId}.Bytes) {{
                yield return b;
            }}
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

            var projects = new List<(string guid, string name, string path)>();
            var cnt = entries.Count;
            var guids = new string[cnt / 100 + 1];
            for (var i = 0; i < cnt; i += 100)
            {
                if (i % 1000 == 0) Console.WriteLine("d"+i);
                var arr = new string[100];
                for (var j = 0; j < 100; j++)
                {
                    if (entries.IsEmpty) break;
                    do
                    {
                        if (!entries.TryTake(out var got)) continue;
                        arr[j] = got;
                        break;
                    } while (true);
                }
                //OEISOutputPart{i}
                Directory.CreateDirectory(
                    $@"C:\Users\Rafael\Documents\GitHub\OEISOutput\TestProject.OEISHarvester.OEISOutputPart{i}");
                File.WriteAllText($@"C:\Users\Rafael\Documents\GitHub\OEISOutput\TestProject.OEISHarvester.OEISOutputPart{i}\TestProject.OEISHarvester.OEISOutputPart{i}.csproj", 
                    @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net47</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include=""..\SuperOEISGenerator\SuperOEISGenerator.csproj"" />
  </ItemGroup>
</Project>
");
                File.WriteAllText($@"C:\Users\Rafael\Documents\GitHub\OEISOutput\TestProject.OEISHarvester.OEISOutputPart{i}\Lib.cs", 
                    Minify(Header + "\n" + string.Join("\n", arr) + "\n" + Footer));
                guids[i / 100] = Guid.NewGuid().ToString();
            }

            var sln = new StringBuilder(@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 2013
VisualStudioVersion = 12.0.0.0
MinimumVisualStudioVersion = 10.0.0.1");
            for (var i = 0; i < cnt; i += 100)
            {
                if (i % 1000 == 0) Console.WriteLine("e"+i);
                var name = $@"TestProject.OEISHarvester.OEISOutputPart{i}";
                var path = $@"TestProject.OEISHarvester.OEISOutputPart{i}\TestProject.OEISHarvester.OEISOutputPart{i}.csproj";
                sln.Append($@"
Project(""{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}"") = ""{name}"", ""{path}"", ""{{{guids[i / 100]}}}""
EndProject");
            }

            sln.Append(@"
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
            
            for (var i = 0; i < cnt; i += 100)
            {
                if (i % 1000 == 0) Console.WriteLine("f"+i);
                var guid = guids[i / 100];
                sln.Append($@"
		{{{guid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{guid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{guid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{guid}}}.Release|Any CPU.Build.0 = Release|Any CPU");
            }

            sln.Append(@"
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
");
            File.WriteAllText(@"C:\Users\Rafael\Documents\GitHub\OEISOutput\OEISOutput.sln", sln.ToString());
        }

        private static string Minify(string s)
        {
            return StartLineTrimmer.Replace(SpaceTrimmer.Replace(s, " "), "$2$3$4$5");
        }
    }
}