// Decompiled with JetBrains decompiler
// Type: TestProj47.Extensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\TestProj47.dll

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace TestProj47
{
    public static partial class Extensions

  {
    /// <summary>Field ReaderSettings.</summary>
    private static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings();

    static Extensions()
    {
      Extensions.ReaderSettings.CheckCharacters = true;
      Extensions.ReaderSettings.ConformanceLevel = ConformanceLevel.Document;
      Extensions.ReaderSettings.ProhibitDtd = false;
      Extensions.ReaderSettings.IgnoreComments = true;
      Extensions.ReaderSettings.IgnoreProcessingInstructions = true;
      Extensions.ReaderSettings.IgnoreWhitespace = true;
      Extensions.ReaderSettings.ValidationFlags = XmlSchemaValidationFlags.None;
      Extensions.ReaderSettings.ValidationType = ValidationType.None;
      Extensions.ReaderSettings.CloseInput = true;
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
      string s = source.Trim();
      if ((int) s[0] != 60 || (int) s[s.Length - 1] != 62)
        return false;
      using (XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(s), Extensions.ReaderSettings))
      {
        try
        {
          do
            ;
          while (xmlReader.Read());
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
        MemoryStream memoryStream1 = new MemoryStream();
        StreamReader streamReader = new StreamReader((Stream) memoryStream1);
        MemoryStream memoryStream2 = memoryStream1;
        XmlWriterSettings settings = new XmlWriterSettings()
        {
          OmitXmlDeclaration = omitXmlDeclaration,
          Indent = true,
          Encoding = (Encoding) new UTF8Encoding(false),
          CloseOutput = true
        };
        using (XmlWriter writer = XmlWriter.Create((Stream) memoryStream2, settings))
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
    public static string ToXmlSyntaxHighlightRtf(this string source, bool indentXml = true, bool omitXmlDeclaration = false, bool darkStyle = false)
    {
      if (string.IsNullOrEmpty(source))
        return source;
      string str1 = indentXml ? source.ToIndentXml(omitXmlDeclaration) : source;
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      for (int index = 0; index < str1.Length; ++index)
      {
        bool flag5 = false;
        if (flag2)
        {
          if ((int) str1[index] == 32)
          {
            stringBuilder.Append(XmlSyntaxHighlightColor.RtfAttributeNameColor(darkStyle));
            flag2 = false;
          }
        }
        else if (flag1 && (int) str1[index] == 34)
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
        if ((int) str1[index] == 60 && !flag4)
        {
          flag1 = true;
          if ((int) str1[index + 1] == 33)
          {
            if ((int) str1[index + 2] == 45 && (int) str1[index + 3] == 45)
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
            if ((int) str1[index + 1] == 63 || (int) str1[index + 1] == 47)
            {
              ++index;
              stringBuilder.Append(str1[index]);
            }
            stringBuilder.Append(XmlSyntaxHighlightColor.RtfTagNameColor(darkStyle));
            flag2 = true;
          }
        }
        bool flag6 = false;
        if ((int) str1[index] == 62)
          flag6 = true;
        if (index < str1.Length - 1 && (int) str1[index + 1] == 62 && ((int) str1[index] == 63 || (int) str1[index] == 47))
          flag6 = true;
        if (flag6)
        {
          if (flag4 && (int) str1[index - 1] == 45 && (int) str1[index - 2] == 45)
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
            if ((int) str1[index] == 47 || (int) str1[index] == 63)
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
      string str2 = stringBuilder.ToString();
      int startIndex1 = str2.IndexOf("{\\colortbl;");
      string str3;
      if (startIndex1 != -1)
      {
        int num = str2.IndexOf('}', startIndex1);
        str3 = str2.Remove(startIndex1, num - startIndex1).Insert(startIndex1, XmlSyntaxHighlightColor.ColorTable);
      }
      else
      {
        int startIndex2 = str2.IndexOf("\\rtf");
        if (startIndex2 < 0)
        {
          str3 = str2.Insert(0, "{\\rtf\\ansi\\deff0" + XmlSyntaxHighlightColor.ColorTable) + "}";
        }
        else
        {
          int startIndex3 = str2.IndexOf('{', startIndex2);
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
      XmlNode element = (XmlNode) (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument).CreateElement(childNode);
      source.AppendChild(element);
      return element;
    }

    /// <summary>Appends a child to a XML node.</summary>
    /// <param name="source">The parent node.</param>
    /// <param name="childNode">The name of the child node.</param>
    /// <param name="namespaceUri">The node namespace.</param>
    /// <returns>The newly created XML node.</returns>
    public static XmlNode CreateChildNode(this XmlNode source, string childNode, string namespaceUri)
    {
      XmlNode element = (XmlNode) (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument).CreateElement(childNode, namespaceUri);
      source.AppendChild(element);
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
      XmlCDataSection cdataSection = (source is XmlDocument ? (XmlDocument) source : source.OwnerDocument).CreateCDataSection(cData);
      source.AppendChild((XmlNode) cdataSection);
      return cdataSection;
    }

    /// <summary>Appends a child to a XML node.</summary>
    /// <param name="childNode">The name of the child node.</param>
    /// <param name="sourceNode">The parent node.</param>
    public static void AppendChildNodeTo(this string childNode, XmlNode sourceNode)
    {
      XmlNode element = (XmlNode) (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument).CreateElement(childNode);
      sourceNode.AppendChild(element);
    }

    /// <summary>Appends a child to a XML node.</summary>
    /// <param name="childNode">The name of the child node.</param>
    /// <param name="sourceNode">The parent node.</param>
    /// <param name="namespaceUri">The node namespace.</param>
    public static void AppendChildNodeTo(this string childNode, XmlNode sourceNode, string namespaceUri)
    {
      XmlNode element = (XmlNode) (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument).CreateElement(childNode, namespaceUri);
      sourceNode.AppendChild(element);
    }

    /// <summary>
    /// Appends a CData section to a XML node and prefills the provided data.
    /// </summary>
    /// <param name="cData">The CData section value.</param>
    /// <param name="sourceNode">The parent node.</param>
    public static void AppendCDataSectionTo(this string cData, XmlNode sourceNode)
    {
      XmlCDataSection cdataSection = (sourceNode is XmlDocument ? (XmlDocument) sourceNode : sourceNode.OwnerDocument).CreateCDataSection(cData);
      sourceNode.AppendChild((XmlNode) cdataSection);
    }

    /// <summary>Returns the value of a nested CData section.</summary>
    /// <param name="source">The parent node.</param>
    /// <returns>The CData section content.</returns>
    public static string GetCDataSection(this XmlNode source)
    {
      foreach (object childNode in source.ChildNodes)
      {
        if (childNode is XmlCDataSection)
          return ((XmlNode) childNode).Value;
      }
      return (string) null;
    }

    /// <summary>Gets an attribute value.</summary>
    /// <param name="source">The node to retrieve the value from.</param>
    /// <param name="attributeName">The Name of the attribute.</param>
    /// <returns>The attribute value.</returns>
    public static string GetAttribute(this XmlNode source, string attributeName)
    {
      return source.GetAttribute(attributeName, (string) null);
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
      XmlAttribute attribute = source.Attributes[attributeName];
      if (attribute == null)
        return defaultValue;
      return attribute.InnerText;
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
      return source.GetAttribute<T>(attributeName, default (T));
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
      string attribute = source.GetAttribute(attributeName);
      if (attribute.IsNullOrEmpty())
        return defaultValue;
      return attribute.ConvertTo<T>(defaultValue, false);
    }

    /// <summary>
    /// Creates or updates an attribute with the passed object.
    /// </summary>
    /// <param name="source">The node to evaluate.</param>
    /// <param name="name">The attribute name.</param>
    /// <param name="value">The attribute value.</param>
    public static void SetAttribute(this XmlNode source, string name, object value)
    {
      Extensions.SetAttribute(source, name, value != null ? value.ToString() : (string) null);
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
      XmlAttribute attribute = source.Attributes[name, source.NamespaceURI];
      if (attribute == null)
      {
        attribute = source.OwnerDocument.CreateAttribute(name, source.OwnerDocument.NamespaceURI);
        source.Attributes.Append(attribute);
      }
      attribute.InnerText = value;
    }
  }
}
