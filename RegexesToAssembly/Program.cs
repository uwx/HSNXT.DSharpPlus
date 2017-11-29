using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace RegexesToAssembly
{
    internal class Program
    {
        private static readonly Regex JsonTypeInfoRegex = new Regex("\\s*\"__type\"\\s*:\\s*\"[^\"]*\"\\s*,\\s*", RegexOptions.Compiled);
        private static readonly Regex IsEmailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.Compiled);
        private static readonly Regex RegexCapitalize = new Regex("\\b([a-z])", RegexOptions.Compiled);
        private static readonly Regex ToLinkRegex = new Regex("(((?<scheme>http(s)?):\\/\\/)?([\\w-]+?\\.\\w+)+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\,]*)?)", RegexOptions.Multiline | RegexOptions.Compiled);
        private static readonly Regex WebUriExpression = new Regex("(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex ObjNotWholePattern = new Regex("[^0-9]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaNumericPattern = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaNumericPatternWhite = new Regex("[^a-zA-Z0-9\\s]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaPatternWhite = new Regex("[^a-zA-Z\\s]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaDashPattern = new Regex("[^a-zA-Z\\-]", RegexOptions.Compiled);
        private static readonly Regex ObjAlphaPattern = new Regex("[^a-zA-Z]", RegexOptions.Compiled);
        private static readonly Regex IsEmailBigRe = new Regex("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", RegexOptions.Compiled);
        
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        private static void Main(string[] args)
        {
            var compilationList = new List<RegexCompilationInfo>
            {
                new RegexCompilationInfo("\\s*\"__type\"\\s*:\\s*\"[^\"]*\"\\s*,\\s*", RegexOptions.Compiled, "JsonTypeInfoRegex", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.Compiled, "IsEmailRegex", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("\\b([a-z])", RegexOptions.Compiled, "RegexCapitalize", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("(((?<scheme>http(s)?):\\/\\/)?([\\w-]+?\\.\\w+)+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\,]*)?)", RegexOptions.Multiline | RegexOptions.Compiled, "ToLinkRegex", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.Compiled | RegexOptions.Singleline, "WebUriExpression", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^0-9]", RegexOptions.Compiled, "ObjNotWholePattern", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^a-zA-Z0-9]", RegexOptions.Compiled, "ObjAlphaNumericPattern", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^a-zA-Z0-9\\s]", RegexOptions.Compiled, "ObjAlphaNumericPatternWhite", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^a-zA-Z\\s]", RegexOptions.Compiled, "ObjAlphaPatternWhite", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^a-zA-Z\\-]", RegexOptions.Compiled, "ObjAlphaDashPattern", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("[^a-zA-Z]", RegexOptions.Compiled, "ObjAlphaPattern", "HSNXT.RegularExpressions", true),
                new RegexCompilationInfo("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", RegexOptions.Compiled, "IsEmailBigRe", "HSNXT.RegularExpressions", true),
            };

            // Apply AssemblyTitle attribute to the new assembly
            //
            // Define the parameter(s) of the AssemblyTitle attribute's constructor 
            Type[] parameters = { typeof(string) };
            // Define the assembly's title
            object[] paramValues = { "General-purpose library of compiled regular expressions" };
            // Get the ConstructorInfo object representing the attribute's constructor
            var ctor = typeof(AssemblyTitleAttribute).GetConstructor(parameters);
            // Create the CustomAttributeBuilder object array
            CustomAttributeBuilder[] attBuilder = { new CustomAttributeBuilder(ctor, paramValues) };

            // Generate assembly with compiled regular expressions
            var compilationArray = new RegexCompilationInfo[compilationList.Count];
            var assemName = new AssemblyName("RegexLib, Version=1.0.0.1002, Culture=neutral, PublicKeyToken=null");
            compilationList.CopyTo(compilationArray);
            Regex.CompileToAssembly(compilationArray, assemName, attBuilder);

//            using (var stream = new FileStream(@"C:\Users\Rafael\Documents\GitHub\TestProject\RegexesToAssembly\RegexesCache.dll", FileMode.OpenOrCreate))
//            {
//                var formatter = new BinaryFormatter();
//
//                formatter.Serialize(stream, Assembly.Load(assemName));
//            }
        }
    }
}