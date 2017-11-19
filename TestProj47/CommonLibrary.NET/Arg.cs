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

namespace HSNXT.ComLib.Arguments
{
    /// <summary>
    /// Attribute to apply to properties of an object which can 
    /// recieve the argument values supplied to a program.
    /// This also describes named arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgAttribute : Attribute
    {
        private string _name = string.Empty;
        /// <summary>
        /// Name of the argument.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _nameLowered = _name != null ? _name.ToLower() : "";
            }
        }


        private string _nameLowered = string.Empty;
        /// <summary>
        /// The name in lowercase.
        /// </summary>
        public string NameLowered => _nameLowered;


        private string _alias = string.Empty;
        /// <summary>
        /// Short alias to represent the name.
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                _aliasLowered = _alias != null ? _alias.ToLower() : "";
            }
        }


        private string _aliasLowered = string.Empty;
        /// <summary>
        /// The alias in lowercase
        /// </summary>
        public string AliasLowered => _aliasLowered;


        /// <summary>
        /// Describes the argument name.
        /// </summary>
        public string Description = string.Empty;


        /// <summary>
        /// Default value.
        /// </summary>
        public object DefaultValue;


        /// <summary>
        /// Date type of the argument.
        /// </summary>
        public Type DataType;


        /// <summary>
        /// Indicates if argument is case sensitive.
        /// </summary>
        public bool IsCaseSensitive;


        /// <summary>
        /// Indicates if is required.
        /// </summary>
        public bool IsRequired = true;


        /// <summary>
        /// The index position of any un-named args.
        /// As in index 0 = a, 1 = b in "-config:Prod a b c"
        /// where a, b, c are unnamed arguments and -config:Prod is a named argument.
        /// </summary>
        public int IndexPosition;


        /// <summary>
        /// Flag to indicate if this value should be interpreted.
        /// </summary>
        public bool Interpret;


        /// <summary>
        /// Whether or not this is used only for development.
        /// </summary>
        public bool IsUsedOnlyForDevelopment;


        /// <summary>
        /// Example value.
        /// </summary>
        public string Example = string.Empty;


        /// <summary>
        /// Example of mutliple various values.
        /// </summary>
        public string ExampleMultiple = string.Empty;


        /// <summary>
        /// Used to group various arguments.
        /// e.g. The tag can be used to separate base/derived argument defintions.
        /// </summary>
        public string Tag = string.Empty;


        /// <summary>
        /// Allow initialize via named property initializers.
        /// </summary>
        public ArgAttribute() { }


        /// <summary>
        /// Initialize using description.
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <param name="alias">A short alias for the argument</param>
        /// <param name="description">Description for the argument</param>
        /// <param name="dataType">The datatype for the argument</param>
        /// <param name="isRequired">Whether or not the argument is required</param>
        /// <param name="defaultValue">A default value</param>
        /// <param name="example">An example for the argument value</param>
        /// <param name="exampleMultiple">A string of multiple examples for the argument value</param>
        /// <param name="interpret">Whether or not the argument value can be interpreted</param>
        /// <param name="isCaseSensitive">Whether or not the argument is case sensitive</param>
        /// <param name="onlyForDevelopment">Whether or not the argument is for development purposes only</param>
        public ArgAttribute(string name, string alias, string description, Type dataType, bool isRequired, object defaultValue = null, string example = "", string exampleMultiple = "",
            bool interpret = false, bool isCaseSensitive = false, bool onlyForDevelopment = false)
        {
            Init(name, description, dataType, isRequired, isCaseSensitive, defaultValue, onlyForDevelopment, example, exampleMultiple);
            Interpret = interpret;
            Alias = alias;
        }


        /// <summary>
        /// Initialize using description.
        /// </summary>
        /// <param name="indexPosition">Index position for the argument</param>
        /// <param name="description">Description for the argument</param>
        /// <param name="dataType">The datatype for the argument</param>
        /// <param name="isRequired">Whether or not the argument is required</param>
        /// <param name="defaultValue">A default value</param>
        /// <param name="example">An example for the argument value</param>
        /// <param name="exampleMultiple">A string of multiple examples for the argument value</param>
        /// <param name="interpret">Whether or not the argument value can be interpreted</param>
        /// <param name="isCaseSensitive">Whether or not the argument is case sensitive</param>
        /// <param name="onlyForDevelopment">Whether or not the argument is for development purposes only</param>
        public ArgAttribute(int indexPosition, string description, Type dataType, bool isRequired, object defaultValue = null, string example = "", string exampleMultiple = "",
            bool interpret = false, bool isCaseSensitive = false, bool onlyForDevelopment = false)
        {
            Init(string.Empty, description, dataType, isRequired, false, defaultValue, false, example, exampleMultiple);
            IndexPosition = indexPosition;
            Interpret = interpret;
        }


        /// <summary>
        /// Initialize all the properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="dataType"></param>
        /// <param name="isRequired"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="defaultValue"></param>
        /// <param name="onlyForDevelopment"></param>
        /// <param name="example"></param>
        /// <param name="exampleMultiple"></param>
        public void Init(string name, string description, Type dataType, bool isRequired, bool isCaseSensitive, object defaultValue,
            bool onlyForDevelopment, string example, string exampleMultiple)
        {
            Name = name;
            Description = description;
            DataType = dataType;
            IsRequired = isRequired;
            IsCaseSensitive = isCaseSensitive;
            DefaultValue = defaultValue;
            IsUsedOnlyForDevelopment = onlyForDevelopment;
            Example = example;
            ExampleMultiple = exampleMultiple;
        }


        /// <summary>
        /// Whether or not this is a named arg as opposed to a position/index based argument.
        /// </summary>
        public bool IsNamed => !string.IsNullOrEmpty(Name);


        /// <summary>
        /// Wehther or not the argument is aliased
        /// </summary>
        public bool IsAliased => !string.IsNullOrEmpty(Alias);


        /// <summary>
        /// Build the usage for this argument.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var val = "";
            var startId = "";

            if (!string.IsNullOrEmpty(Name))
                startId = "-" + Name + " : ";
            else
                startId = "- index[" + IndexPosition + "] : ";

            val += string.IsNullOrEmpty(Description) ? string.Empty : Description + ", ";
            val += IsRequired ? "Required" : "Optional";
            val += ", " + DataType.Name + ", ";
            val += IsCaseSensitive ? "Case Sensitive" : "Not CaseSensitive";
            val += DefaultValue != null ? ", default to : " + DefaultValue : string.Empty;
            val = startId + val;
            return val;
        }
    }
}
