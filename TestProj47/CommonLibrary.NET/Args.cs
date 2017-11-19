
/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace HSNXT.ComLib.Arguments
{
   /// <summary>
    /// Class to parse arguments and store both named and positional arguments for easy lookup.
    /// </summary>
    /// <example>
    /// <para>
    ///     
    /// </para>
    /// </example>
    public partial class Args
    {
        #region Private Data
        /// <summary>
        /// Index position of meta request.
        /// e.g. -help, -pause, -version
        /// </summary>
        private int _metaIndex;
        #endregion


        #region Public Properties
        /// <summary>
        /// The argument schema containing what options are available.
        /// </summary>
        public ArgsSchema Schema { get; private set; }


        /// <summary>
        /// Named args prefix used. "-"
        /// e.g. -env:Production
        /// </summary>
        public string Prefix { get; set; }


        /// <summary>
        /// Named args key / value separator used. ":"
        /// e.g. -env:Production
        /// </summary>
        public string Separator { get; set; }


        /// <summary>
        /// <para>
        /// Collection of named arguments.
        /// e.g. If "-config:prod.xml -date:T-1 MyApplicationId" is supplied to command line.
        /// 
        /// Named["config"] = "prod.xml"
        /// Named["date"] = "T-1"
        /// </para>
        /// </summary>
        public Dictionary<string, string> Named = new Dictionary<string, string>();


        /// <summary>
        /// <para>
        /// Collection of un-named arguments supplied to command line.
        /// e.g. If "-config:prod.xml -date:T-1 MyApplicationId" is supplied to command line.
        /// 
        /// Positional[0] = "MyApplicationId"
        /// </para>
        /// </summary>
        public List<string> Positional = new List<string>();


        /// <summary>
        /// The original/raw arguments that were supplied.
        /// </summary>
        public string[] Raw;
        #endregion


        #region Constructors
        /// <summary>
        /// Create new instance using defaults
        /// </summary>
        public Args()
            : this("-", ":")
        {
        }


        /// <summary>
        /// Default construction.
        /// </summary>
        public Args(string[] args) 
            : this(args, null)
        {            
        }


        /// <summary>
        /// Initialize only the prefix / separator.
        /// </summary>
        public Args(string prefix, string separator)            
        {
            Prefix = prefix;
            Separator = separator;
            Schema = new ArgsSchema(this);
        }


        /// <summary>
        /// Default construction.
        /// </summary>
        public Args(string[] args, string prefix, string separator)
            : this(args, prefix, separator, null, null, null)
        {
        }


        /// <summary>
        /// Default construction.
        /// </summary>
        public Args(string[] args, List<ArgAttribute> supported) 
            : this(args, "-", ":", supported, null, null)
        {
        }


        /// <summary>
        /// Default construction.
        /// </summary>
        public Args(string[] args, string prefix, string separator, List<ArgAttribute> supported)
            : this(args, prefix, separator, supported, null, null)
        {
        }


        /// <summary>
        /// Initialize arguments.
        /// </summary>
        /// <param name="args">Raw arguments from command line.</param>
        /// <param name="supported">Supported named/positional argument definitions.</param>
        /// <param name="named">Named arguments</param>
        /// <param name="positional">Positional arguments.</param>
        public Args(string[] args, List<ArgAttribute> supported, Dictionary<string, string> named, List<string> positional)
            : this(args, "-", ":", supported, named, positional)
        {
        }


        /// <summary>
        /// Initialize arguments.
        /// </summary>
        /// <param name="args">Raw arguments from command line.</param>
        /// <param name="prefix">Prefix used for named arguments. "-".</param>
        /// <param name="keyValueSeparator">Separator used for named arguments key/values. ":".</param>
        /// <param name="supported">Supported named/positional argument definitions.</param>
        /// <param name="named">Named arguments</param>
        /// <param name="positional">Positional arguments.</param>
        public Args(string[] args, string prefix, string keyValueSeparator, List<ArgAttribute> supported, Dictionary<string, string> named, List<string> positional)
        {            
            Raw = args;            
            Prefix = prefix;
            Separator = keyValueSeparator;
            Schema = new ArgsSchema(this);
            if (supported != null) Schema.Items = supported; 
            if (named != null) Named = named;
            if (positional != null) Positional = positional;            
        }
        #endregion


        #region Parse methods
        /// <summary>
        /// Parse the raw arguments using the internal schema.
        /// </summary>
        /// <param name="rawArgs"></param>
        /// <returns></returns>
        public BoolMessage DoParse(string[] rawArgs)
        {
            var result =Parse(rawArgs, Prefix, Separator, Schema.Items);

            // Transfer the Positional and Named back over to this instance.
            if (result.Success)
            {
                this.Positional = result.Item.Positional;
                this.Named = result.Item.Named;
            }
            this.Raw = rawArgs;
            return result;
        }
        #endregion


        #region Convenience methods
        /// <summary>
        /// Whether or not the named argument exists.
        /// </summary>
        /// <param name="namedArg"></param>
        /// <returns></returns>
        public bool Contains(string namedArg)
        {
            return Named != null && Named.ContainsKey(namedArg);
        }


        /// <summary>
        /// Whether or not the there is a named argument/value associated with the supplied argument.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public bool Contains(ArgAttribute arg)
        {
            if (Named == null || Named.Count == 0) return false;

            if (Named.ContainsKey(arg.Name)) return true;
            if (Named.ContainsKey(arg.Alias)) return true;
            if (arg.IsCaseSensitive) return false;
            if (Named.ContainsKey(arg.NameLowered)) return true;
            return Named.ContainsKey(arg.AliasLowered);
        }


        /// <summary>
        /// Get the named argument specified by <paramref name="argName"/>
        /// throws an exception if the named argument is not present.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argName"></param>
        /// <returns></returns>
        public T Get<T>(string argName)
        {
            var isSchemaPopulated = IsSchemaPopulated;
            var hasArg = Named.ContainsKey(argName);
            var errorMsg = "There is no named argument : " + argName;

            // No schema
            if (!isSchemaPopulated && !hasArg) throw new ArgumentException(errorMsg);
            if (!isSchemaPopulated) return GetNamedVal<T>(argName);
            
            // Schema
            var argAttrib = this.Schema.GetNamed(argName);

            // No arg in schema and no arg in values either.
            if (argAttrib == null && !hasArg) throw new ArgumentException(errorMsg);
            if (argAttrib != null && Named.ContainsKey(argAttrib.Name)) return GetNamedVal<T>(argAttrib.Name);
            if (Named.ContainsKey(argAttrib.Alias)) return GetNamedVal<T>(argAttrib.Alias);

            // Check for case-sensitivity.
            var allowLCase = !argAttrib.IsCaseSensitive;
            if (!allowLCase) throw new ArgumentException(errorMsg);
            if (Named.ContainsKey(argAttrib.NameLowered)) return GetNamedVal<T>(argAttrib.NameLowered);
            if (Named.ContainsKey(argAttrib.AliasLowered)) return GetNamedVal<T>(argAttrib.AliasLowered);

            // No name or alias at his point.
            throw new ArgumentException(errorMsg);
        }


        /// <summary>
        /// Get the named argument specified by <paramref name="argName"/>
        /// if it exists, returns <paramref name="defaultValue"/> otherwise.
        /// </summary>
        /// <param name="argName">Name of the named argument.</param>
        /// <param name="defaultValue">Default value to return if named arg does not exist.</param>
        /// <returns></returns>
        public T Get<T>(string argName, T defaultValue)
        {
            var isSchemaPopulated = IsSchemaPopulated;
            var hasArg = Named.ContainsKey(argName);
            var val = string.Empty;

            // No schema
            if (!isSchemaPopulated && !hasArg) return defaultValue;
            if (!isSchemaPopulated && hasArg) return GetNamedValWithDefault(argName, defaultValue);

            // Schema
            var argAttrib = this.Schema.GetNamed(argName);

            // No arg in schema and no arg in values either.
            if (argAttrib == null && !hasArg) return defaultValue;
            if (Named.ContainsKey(argAttrib.Name)) return GetNamedValWithDefault(argAttrib.Name, defaultValue);
            if (Named.ContainsKey(argAttrib.Alias)) return GetNamedValWithDefault(argAttrib.Alias, defaultValue);
            
            // Check for case-sensitivity.
            var allowLCase = !argAttrib.IsCaseSensitive;
            if (!allowLCase) return defaultValue;
            if (Named.ContainsKey(argAttrib.NameLowered)) return GetNamedValWithDefault(argAttrib.NameLowered, defaultValue);
            return Named.ContainsKey(argAttrib.AliasLowered) ? GetNamedValWithDefault(argAttrib.AliasLowered, defaultValue) : defaultValue;
        }


        /// <summary>
        /// Get the positional argument specified by <paramref name="indexPosition"/>
        /// throws an exception if the positional argument is not present.
        /// </summary>
        /// <param name="indexPosition">Index position of the argument.</param>
        /// <returns></returns>
        public T Get<T>(int indexPosition)
        {
            if (Positional == null || Positional.Count == 0)
                throw new ArgumentException("There are 0 positional arguments");

            var val = Positional[indexPosition];
            var converted = Converter.ConvertTo<T>(val);
            return converted;
        }


        /// <summary>
        /// Get the positional argument specified by <paramref name="indexPosition"/>
        /// if it exists, returns <paramref name="defaultValue"/> otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexPosition"></param>
        /// <param name="defaultValue">Default value to return if named arg does not exist.</param>
        /// <returns></returns>
        public T Get<T>(int indexPosition, T defaultValue)
        {
            if(Positional == null || Positional.Count <= indexPosition)
                return defaultValue;

            var val = Positional[indexPosition];
            if (string.IsNullOrEmpty(val))
                return defaultValue;

            var converted = Converter.ConvertTo<T>(val);
            return converted;
        }
        #endregion


        #region Checks
        /// <summary>
        /// True if there are 0 arguments.
        /// </summary>
        public bool IsEmpty => Raw == null || Raw.Length == 0;


        /// <summary>
        /// Returns true if there is only 1 argument with value:  --Version -Version /Version
        /// </summary>
        /// <returns></returns>
        public bool IsVersion => IsPositionalArg(_metaIndex, "version");


        /// <summary>
        /// Returns true if there is only 1 argument with value: --pause -pause /pause
        /// </summary>
        /// <returns></returns>
        public bool IsPause => IsPositionalArg(_metaIndex, "pause");


        /// <summary>
        /// Returns true if there is only 1 argument with value: --help -help --help -help /? -? ?
        /// </summary>
        /// <returns></returns>
        public bool IsHelp => IsPositionalArg(_metaIndex, "help", "?", "/?", "-?");


        /// <summary>
        /// Returns true if there is only 1 argument with value: --About -About /About
        /// </summary>
        public bool IsInfo => IsPositionalArg(_metaIndex, "about") || IsPositionalArg(_metaIndex, "info");

        #endregion


        #region Usage
        /// <summary>
        /// Show the usage of the arguments on the console.
        /// </summary>
        public void ShowUsage(string appName)
        {
            var usage = GetUsage(appName);
            Console.WriteLine(usage);
        }


        /// <summary>
        /// Get a string representing the usage of the arguments.
        /// </summary>
        /// <returns></returns>
        public string GetUsage(string appName)
        {
            if (Schema.IsEmpty) return "Argument definitions are not present.";

            var usage = ArgsUsage.Build(appName, Schema.Items, Prefix, Separator);
            return usage;
        }
        #endregion


        /// <summary>
        /// Set the index position of the argument indicating a specific meta query.
        /// Such as "-help", "-version", "-about".
        /// e.g. if 0, this indicates that the argument "-help" should be expected at position 0
        /// in the raw arguments.
        /// </summary>
        /// <param name="ndx"></param>
        public void SetMetaIndex(int ndx)
        {
            if(ndx >= 0) _metaIndex = ndx;
        }

        
        /// <summary>
        /// Checks if the first positional arg in the raw arguments is equal to 
        /// what's provided.
        /// </summary>
        /// <param name="metaIndex"></param>
        /// <param name="valToCheck"></param>
        /// <param name="additionalValues"></param>
        /// <returns></returns>
        private bool IsPositionalArg(int metaIndex, string valToCheck, params string[] additionalValues)
        {
            var args = Raw;
            valToCheck = valToCheck.ToLower().Trim();

            // Check for help
            if (args == null || args.Length < 1) return false;
            var val = args[metaIndex].ToLower();
            if (val == Prefix + valToCheck || val == "-" + valToCheck  || val == "--" + valToCheck || val == "/" + valToCheck || val == valToCheck)
                return true;

            if (additionalValues == null || additionalValues.Length <= 0) return false;
            return additionalValues.Any(additionalVal => additionalVal == val);
        }


        private bool IsSchemaPopulated => this.Schema != null && this.Schema.Count != 0;


        private T GetNamedVal<T>(string argName)
        {
            var val = Named[argName];
            if (string.IsNullOrEmpty(val))
                throw new ArgumentException("The named argument : " + argName + " is empty");

            var converted = Converter.ConvertTo<T>(val);
            return converted;
        }


        private T GetNamedValWithDefault<T>(string argName, T defaultValue)
        {
            var val = Named[argName];
            if (string.IsNullOrEmpty(val))
                return defaultValue;

            var converted = Converter.ConvertTo<T>(val);
            return converted;
        }
    }
}
