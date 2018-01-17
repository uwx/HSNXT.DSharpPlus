// Decompiled with JetBrains decompiler
// Type: TestProj47.Extensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Field ReaderSettings.</summary>
        private static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings();

        static Extensions()
        {
            ReaderSettings.CheckCharacters = true;
            ReaderSettings.ConformanceLevel = ConformanceLevel.Document;
            ReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
            ReaderSettings.IgnoreComments = true;
            ReaderSettings.IgnoreProcessingInstructions = true;
            ReaderSettings.IgnoreWhitespace = true;
            ReaderSettings.ValidationFlags = XmlSchemaValidationFlags.None;
            ReaderSettings.ValidationType = ValidationType.None;
            ReaderSettings.CloseInput = true;
            
            LookupTableLower = new char[256][];
            LookupTableUpper = new char[256][];
            for (var i = 0; i < 256; i++)
            {
                LookupTableLower[i] = i.ToString("x2").ToCharArray();
                LookupTableUpper[i] = i.ToString("X2").ToCharArray();
            }
        }

        /// <summary>
        /// Determines whether the source string is valid Xml string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>true if string is valid Xml string; otherwise, false.</returns>
        public static bool IsValidXml(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;
            var s = source.Trim();
            if (s[0] != 60 || s[s.Length - 1] != 62)
                return false;
            using (var xmlReader = XmlReader.Create(new StringReader(s), ReaderSettings))
            {
                try
                {
                    while (xmlReader.Read())
                    {
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>Converts valid Xml string to the indent Xml string.</summary>
        /// <param name="source">The source Xml string.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <returns>Indent Xml string.</returns>
        public static string ToIndentXml(this string source, bool omitXmlDeclaration = false)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            if (!source.IsValidXml())
                return source;
            try
            {
                var memoryStream1 = new MemoryStream();
                var streamReader = new StreamReader(memoryStream1);
                var memoryStream2 = memoryStream1;
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    Indent = true,
                    Encoding = new UTF8Encoding(false),
                    CloseOutput = true
                };
                using (var writer = XmlWriter.Create(memoryStream2, settings))
                {
                    XDocument.Parse(source).Save(writer);
                    writer.Flush();
                    memoryStream1.Position = 0L;
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// Converts RTF string to the Xml syntax highlight RTF string.
        /// </summary>
        /// <param name="source">The source RTF string.</param>
        /// <param name="indentXml">true to indent Xml string; otherwise, keep the original string.</param>
        /// <param name="omitXmlDeclaration">Whether to write an Xml declaration.</param>
        /// <param name="darkStyle">true to use dark style; otherwise, use light style.</param>
        /// <returns>The Xml syntax highlight RTF string.</returns>
        public static string ToXmlSyntaxHighlightRtf(this string source, bool indentXml = true,
            bool omitXmlDeclaration = false, bool darkStyle = false)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            var str1 = indentXml ? source.ToIndentXml(omitXmlDeclaration) : source;
            var stringBuilder = new StringBuilder(string.Empty);
            var flag1 = false;
            var flag2 = false;
            var flag3 = false;
            var flag4 = false;
            for (var index = 0; index < str1.Length; ++index)
            {
                var flag5 = false;
                if (flag2)
                {
                    if (str1[index] == 32)
                    {
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfAttributeNameColor(darkStyle));
                        flag2 = false;
                    }
                }
                else if (flag1 && str1[index] == 34)
                {
                    if (flag3)
                    {
                        stringBuilder.Append(str1[index]);
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfAttributeNameColor(darkStyle));
                        flag5 = true;
                        flag3 = false;
                    }
                    else
                    {
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfAttributeValueColor(darkStyle));
                        flag3 = true;
                    }
                }
                if (str1[index] == 60 && !flag4)
                {
                    flag1 = true;
                    if (str1[index + 1] == 33)
                    {
                        if (str1[index + 2] == 45 && str1[index + 3] == 45)
                        {
                            stringBuilder.Append(XmlSyntaxHighlightColor.RtfCommentColor(darkStyle));
                            flag4 = true;
                        }
                        else
                        {
                            stringBuilder.Append(XmlSyntaxHighlightColor.RtfTagNameColor(darkStyle));
                            flag2 = true;
                        }
                    }
                    if (!flag4)
                    {
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfTagColor(darkStyle));
                        stringBuilder.Append(str1[index]);
                        flag5 = true;
                        if (str1[index + 1] == 63 || str1[index + 1] == 47)
                        {
                            ++index;
                            stringBuilder.Append(str1[index]);
                        }
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfTagNameColor(darkStyle));
                        flag2 = true;
                    }
                }
                var flag6 = str1[index] == 62 || index < str1.Length - 1 && str1[index + 1] == 62 &&
                            (str1[index] == 63 || str1[index] == 47);
                if (flag6)
                {
                    if (flag4 && str1[index - 1] == 45 && str1[index - 2] == 45)
                    {
                        stringBuilder.Append(str1[index]);
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfDefaultColor(darkStyle));
                        flag5 = true;
                        flag4 = false;
                        flag1 = false;
                    }
                    if (flag1)
                    {
                        stringBuilder.Append("\\cf1 ");
                        if (str1[index] == 47 || str1[index] == 63)
                            stringBuilder.Append(str1[index++]);
                        stringBuilder.Append(str1[index]);
                        stringBuilder.Append(XmlSyntaxHighlightColor.RtfDefaultColor(darkStyle));
                        flag5 = true;
                        flag2 = false;
                        flag1 = false;
                    }
                }
                if (!flag5)
                    stringBuilder.Append(str1[index]);
            }
            var str2 = stringBuilder.ToString();
            var startIndex1 = str2.IndexOfInvariant(@"{\colortbl;");
            string str3;
            if (startIndex1 != -1)
            {
                var num = str2.IndexOf('}', startIndex1);
                str3 = str2.Remove(startIndex1, num - startIndex1)
                    .Insert(startIndex1, XmlSyntaxHighlightColor.ColorTable);
            }
            else
            {
                var startIndex2 = str2.IndexOfInvariant(@"\rtf");
                if (startIndex2 < 0)
                {
                    str3 = str2.Insert(0, @"{\rtf\ansi\deff0" + XmlSyntaxHighlightColor.ColorTable) + "}";
                }
                else
                {
                    var startIndex3 = str2.IndexOf('{', startIndex2);
                    if (startIndex3 == -1)
                        startIndex3 = str2.IndexOf('}', startIndex2) - 1;
                    str3 = str2.Insert(startIndex3, XmlSyntaxHighlightColor.ColorTable);
                }
            }
            return str3;
        }

        /// <summary>Appends a child to a XML node.</summary>
        /// <param name="source">The parent node.</param>
        /// <param name="childNode">The name of the child node.</param>
        /// <returns>The newly created XML node.</returns>
        public static XmlNode CreateChildNode(this XmlNode source, string childNode)
        {
            XmlNode element =
                (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument)?.CreateElement(childNode);
            source.AppendChild(element ?? throw new ArgumentException(nameof(element)));
            return element;
        }

        /// <summary>Appends a child to a XML node.</summary>
        /// <param name="source">The parent node.</param>
        /// <param name="childNode">The name of the child node.</param>
        /// <param name="namespaceUri">The node namespace.</param>
        /// <returns>The newly created XML node.</returns>
        public static XmlNode CreateChildNode(this XmlNode source, string childNode, string namespaceUri)
        {
            XmlNode element =
                (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument)?.CreateElement(childNode,
                    namespaceUri);
            source.AppendChild(element ?? throw new ArgumentException(nameof(element)));
            return element;
        }

        /// <summary>Appends a CData section to a XML node.</summary>
        /// <param name="source">The parent node.</param>
        /// <returns>The created CData Section.</returns>
        public static XmlCDataSection CreateCDataSection(this XmlNode source)
        {
            return source.CreateCDataSection(string.Empty);
        }

        /// <summary>
        /// Appends a CData section to a XML node and prefills the provided data.
        /// </summary>
        /// <param name="source">The parent node.</param>
        /// <param name="cData">The CData section value.</param>
        /// <returns>The created CData Section.</returns>
        public static XmlCDataSection CreateCDataSection(this XmlNode source, string cData)
        {
            var cdataSection =
                (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument)?.CreateCDataSection(cData);
            source.AppendChild(cdataSection ?? throw new ArgumentException(nameof(cdataSection)));
            return cdataSection;
        }

        /// <summary>Appends a child to a XML node.</summary>
        /// <param name="childNode">The name of the child node.</param>
        /// <param name="sourceNode">The parent node.</param>
        public static void AppendChildNodeTo(this string childNode, XmlNode sourceNode)
        {
            XmlNode element = (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument)
                ?.CreateElement(childNode);
            sourceNode.AppendChild(element ?? throw new ArgumentException(nameof(element)));
        }

        /// <summary>Appends a child to a XML node.</summary>
        /// <param name="childNode">The name of the child node.</param>
        /// <param name="sourceNode">The parent node.</param>
        /// <param name="namespaceUri">The node namespace.</param>
        public static void AppendChildNodeTo(this string childNode, XmlNode sourceNode, string namespaceUri)
        {
            XmlNode element =
                (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument)?.CreateElement(
                    childNode, namespaceUri);
            sourceNode.AppendChild(element ?? throw new ArgumentException(nameof(element)));
        }

        /// <summary>
        /// Appends a CData section to a XML node and prefills the provided data.
        /// </summary>
        /// <param name="cData">The CData section value.</param>
        /// <param name="sourceNode">The parent node.</param>
        public static void AppendCDataSectionTo(this string cData, XmlNode sourceNode)
        {
            var cdataSection = (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument)
                ?.CreateCDataSection(cData);
            sourceNode.AppendChild(cdataSection ?? throw new ArgumentException(nameof(cdataSection)));
        }

        /// <summary>Returns the value of a nested CData section.</summary>
        /// <param name="source">The parent node.</param>
        /// <returns>The CData section content.</returns>
        public static string GetCDataSection(this XmlNode source)
        {
            return source.ChildNodes.OfType<XmlCDataSection>().Select(childNode => childNode.Value).FirstOrDefault();
        }

        /// <summary>Gets an attribute value.</summary>
        /// <param name="source">The node to retrieve the value from.</param>
        /// <param name="attributeName">The Name of the attribute.</param>
        /// <returns>The attribute value.</returns>
        public static string GetAttribute(this XmlNode source, string attributeName)
        {
            return source.GetAttribute(attributeName, null);
        }

        /// <summary>
        /// Gets an attribute value
        /// If the value is empty, uses the specified default value.
        /// </summary>
        /// <param name="source">The node to retrieve the value from.</param>
        /// <param name="attributeName">The Name of the attribute.</param>
        /// <param name="defaultValue">The default value to be returned if no matching attribute exists.</param>
        /// <returns>The attribute value.</returns>
        public static string GetAttribute(this XmlNode source, string attributeName, string defaultValue)
        {
            var attribute = source.Attributes?[attributeName];
            return attribute?.InnerText ?? defaultValue;
        }

        /// <summary>
        /// Gets an attribute value converted to the specified data type.
        /// </summary>
        /// <typeparam name="T">The desired return data type.</typeparam>
        /// <param name="source">The node to evaluate.</param>
        /// <param name="attributeName">The Name of the attribute.</param>
        /// <returns>The attribute value.</returns>
        public static T GetAttribute<T>(this XmlNode source, string attributeName)
        {
            return source.GetAttribute(attributeName, default(T));
        }

        /// <summary>
        /// Gets an attribute value converted to the specified data type
        /// If the value is empty, uses the specified default value.
        /// </summary>
        /// <typeparam name="T">The desired return data type.</typeparam>
        /// <param name="source">The node to evaluate.</param>
        /// <param name="attributeName">The Name of the attribute.</param>
        /// <param name="defaultValue">The default value to be returned if no matching attribute exists.</param>
        /// <returns>The attribute value.</returns>
        public static T GetAttribute<T>(this XmlNode source, string attributeName, T defaultValue)
        {
            var attribute = source.GetAttribute(attributeName);
            return attribute.IsNullOrEmpty() ? defaultValue : attribute.ConvertTo(defaultValue);
        }

        /// <summary>
        /// Creates or updates an attribute with the passed object.
        /// </summary>
        /// <param name="source">The node to evaluate.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        public static void SetAttribute(this XmlNode source, string name, object value)
        {
            source.SetAttribute(name, value?.ToString());
        }

        /// <summary>
        /// Creates or updates an attribute with the passed value.
        /// </summary>
        /// <param name="source">The node to evaluate.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        public static void SetAttribute(this XmlNode source, string name, string value)
        {
            if (source == null)
                return;
            var attribute = source.Attributes?[name, source.NamespaceURI];
            if (attribute == null)
            {
                attribute = source.OwnerDocument?.CreateAttribute(name, source.OwnerDocument.NamespaceURI);
                source.Attributes?.Append(attribute ?? throw new ArgumentException(nameof(attribute)));
            }
            if (attribute == null) throw new ArgumentException(nameof(attribute));
            attribute.InnerText = value;
        }
    }
}