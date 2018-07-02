using System.Collections.Generic;

namespace DSharpPlus.ModernEmbedBuilder
{
    public class DuckField
    {
        /// <summary>
        /// Implicitly create a DuckField with string name.
        /// </summary>
        public static implicit operator DuckField(string name) => new DuckField()
        {
            Name = name
        };
        
        /// <summary>
        /// Implicitly create a DuckField with string name and string value.
        /// </summary>
        public static implicit operator DuckField((string name, string value) nameAndValue) => new DuckField()
        {
            Name = nameAndValue.name,
            Value = nameAndValue.value,
        };
        
        /// <summary>
        /// Implicitly create a DuckField with string name.
        /// </summary>
        public static implicit operator DuckField((string name, string value, bool inline) everything) => new DuckField()
        {
            Name = everything.name,
            Value = everything.value,
            Inline = everything.inline,
        };
        
        /// <summary>
        /// Implicitly create a DuckField with string name.
        /// </summary>
        public static implicit operator DuckField(Dictionary<string, string> dict) => new DuckField()
        {
            Name = dict.ContainsKey("name") ? dict["name"] : null,
            Value = dict.ContainsKey("name") ? dict["name"] : null,
            Inline = dict.ContainsKey("name") && dict["name"] == "true",
        };
        
        /// <summary>
        /// Implicitly create a DuckField with string name.
        /// </summary>
        public static implicit operator DuckField(List<string> list) => new DuckField()
        {
            Name = list.Count > 0 ? list[0] : null,
            Value = list.Count > 1 ? list[1] : null,
            Inline = list.Count > 2 && list[2] == "true",
        };
        
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets whether or not this field should display inline.
        /// </summary>
        public bool Inline { get; set; }

        public DuckField()
        {
            
        }

        public DuckField(string name, string value = null, bool inline = false)
        {
            this.Name = name;
            this.Value = value;
            this.Inline = inline;
        }
    }
}