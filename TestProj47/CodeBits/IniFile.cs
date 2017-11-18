#region --- License & Copyright Notice ---

/*
CodeBits Code Snippets
Copyright (c) 2012-2017 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

#endregion

/* Documentation: https://github.com/JeevanJames/CodeBits/wiki/IniFile */

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HSNXT
{
    public sealed class IniFile : KeyedCollection<string, IniFile.Section>
    {
        private readonly StringComparison _comparison;

        #region Construction

        public IniFile() : this(null)
        {
        }

        public IniFile(IniLoadSettings settings)
        {
            settings = settings ?? IniLoadSettings.Default;
            _comparison = settings.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        }

        public IniFile(string iniFilePath, IniLoadSettings settings)
        {
            if (iniFilePath == null)
                throw new ArgumentNullException(nameof(iniFilePath));
            if (!File.Exists(iniFilePath))
                throw new ArgumentException($"INI file '{iniFilePath}' does not exist", nameof(iniFilePath));

            settings = settings ?? IniLoadSettings.Default;
            _comparison = settings.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            using (var reader =
                new StreamReader(iniFilePath, settings.Encoding ?? Encoding.UTF8, settings.DetectEncoding))
                ParseIniFile(reader);
        }

        public IniFile(Stream stream, IniLoadSettings settings)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead)
                throw new ArgumentException("Cannot read from specified stream", nameof(stream));

            settings = settings ?? IniLoadSettings.Default;
            _comparison = settings.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            using (var reader = new StreamReader(stream, settings.Encoding ?? Encoding.UTF8, settings.DetectEncoding))
                ParseIniFile(reader);
        }

        public static IniFile Load(string content, IniLoadSettings settings)
        {
            settings = settings ?? IniLoadSettings.Default;
            var encoding = settings.Encoding ?? Encoding.UTF8;

            var contentBytes = encoding.GetBytes(content);
            var stream = new MemoryStream(contentBytes.Length);
            stream.Write(contentBytes, 0, contentBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return new IniFile(stream, settings);
        }

        private void ParseIniFile(TextReader reader)
        {
            Section currentSection = null;
            for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                //Blank line
                if (string.IsNullOrEmpty(line) || line.Trim().Length == 0)
                    continue;

                line = line.Trim();

                //Comment
                if (line.StartsWith(";"))
                    continue;

                //Section
                var sectionMatch = SectionPattern.Match(line);
                if (sectionMatch.Success)
                {
                    var sectionName = sectionMatch.Groups[1].Value;
                    if (this.Any(section => section.Name.Equals(sectionName, _comparison)))
                        throw new NotSupportedException($"Duplicate section found - '{sectionName}'");
                    currentSection = new Section(sectionName);
                    Add(currentSection);
                    continue;
                }

                //Property
                var propertyMatch = PropertyPattern.Match(line);
                if (!propertyMatch.Success) throw new NotSupportedException($"Unrecognized line '{line}'");
                var propertyName = propertyMatch.Groups[1].Value;
                var propertyValue = propertyMatch.Groups[2].Value;

                if (currentSection == null)
                    throw new NotSupportedException($"Property '{propertyName}' is not part of any section");
                if (currentSection.Any(property => property.Key.Equals(propertyName, _comparison)))
                    throw new NotSupportedException(
                        $"Key '{propertyName}' already exists in section '{currentSection.Name}'");

                currentSection.Add(propertyName, propertyValue);
            }
        }

        #endregion

        public void SaveTo(string filePath)
        {
            using (var writer = File.CreateText(filePath))
                SaveTo(writer);
        }

        public void SaveTo(Stream stream)
        {
            using (var writer = new StreamWriter(stream))
                SaveTo(writer);
        }

        public void SaveTo(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            InternalSave(writer.WriteLine, writer.WriteLine);
            writer.Flush();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            InternalSave((str, args) => sb.AppendFormat(str, args), () => sb.AppendLine());
            return sb.ToString();
        }

        private void InternalSave(Action<string, object[]> writeAction, Action writeBlankLine)
        {
            foreach (var section in this)
            {
                writeAction("[{0}]", new object[] {section.Name});
                writeBlankLine();
                foreach (var property in section)
                {
                    writeAction("{0}={1}", new object[] {property.Key, property.Value});
                    writeBlankLine();
                }
                writeBlankLine();
            }
        }

        protected override string GetKeyForItem(Section item)
        {
            return item.Name;
        }

        private static readonly Regex SectionPattern = new Regex(@"^\[\s*(\w[\w\s]*)\s*\]$");
        private static readonly Regex PropertyPattern = new Regex(@"^(\w[\w\s]+\w)\s*=(.*)$");

        public const string PropertyFormat = PropertyKeyFormat + @"\s*=" + PropertyValueFormat;
        public const string PropertyKeyFormat = @"(\w[\w\s]+\w)";
        public const string PropertyValueFormat = @"(.*)";

        #region Section

        public sealed class Section : Collection<Property>
        {
            public Section(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public void Add(string key, string value)
            {
                Add(new Property
                {
                    Key = key,
                    Value = value
                });
            }

            public bool Remove(string key)
            {
                for (var i = 0; i < Count; i++)
                {
                    var property = this[i];
                    if (key == property.Key)
                    {
                        RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }

            public string this[string key]
            {
                get
                {
                    var matchingProperty = this.FirstOrDefault(p => p.Key == key);
                    return matchingProperty?.Value;
                }
                set => Add(key, value);
            }
        }

        #endregion

        #region Property

        public sealed class Property
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }

    public sealed class IniLoadSettings
    {
        public IniLoadSettings()
        {
            Encoding = Encoding.UTF8;
        }

        public Encoding Encoding { get; set; }

        public bool DetectEncoding { get; set; }

        public bool CaseSensitive { get; set; }

        public static readonly IniLoadSettings Default = new IniLoadSettings();
    }
}