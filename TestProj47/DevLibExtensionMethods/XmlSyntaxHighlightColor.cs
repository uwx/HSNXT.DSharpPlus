// Decompiled with JetBrains decompiler
// Type: TestProj47.XmlSyntaxHighlightColor
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System.Text;

namespace HSNXT
{
    /// <summary>Xml syntax highlight color.</summary>
    internal static class XmlSyntaxHighlightColor
    {
        /// <summary>Represents light style RTF tag color.</summary>
        public const string RtfLightTagColor = "\\cf1 ";

        /// <summary>Represents light style RTF tag name color.</summary>
        public const string RtfLightTagNameColor = "\\cf2 ";

        /// <summary>Represents light style RTF attribute name color.</summary>
        public const string RtfLightAttributeNameColor = "\\cf3 ";

        /// <summary>Represents light style RTF attribute value color.</summary>
        public const string RtfLightAttributeValueColor = "\\cf4 ";

        /// <summary>Represents light style RTF comment color.</summary>
        public const string RtfLightCommentColor = "\\cf5 ";

        /// <summary>Represents light style RTF default color.</summary>
        public const string RtfLightDefaultColor = "\\cf6 ";

        /// <summary>Represents dark style RTF tag color.</summary>
        public const string RtfDarkTagColor = "\\cf7 ";

        /// <summary>Represents dark style RTF tag name color.</summary>
        public const string RtfDarkTagNameColor = "\\cf8 ";

        /// <summary>Represents dark style RTF attribute name color.</summary>
        public const string RtfDarkAttributeNameColor = "\\cf9 ";

        /// <summary>Represents dark style RTF attribute value color.</summary>
        public const string RtfDarkAttributeValueColor = "\\cf10 ";

        /// <summary>Represents dark style RTF comment color.</summary>
        public const string RtfDarkCommentColor = "\\cf11 ";

        /// <summary>Represents dark style RTF default color.</summary>
        public const string RtfDarkDefaultColor = "\\cf12 ";

        /// <summary>Field LightTagColor.</summary>
        private const string LightTagColor = "\\red0\\green0\\blue255";

        /// <summary>Field LightTagNameColor.</summary>
        private const string LightTagNameColor = "\\red163\\green21\\blue21";

        /// <summary>Field LightAttributeNameColor.</summary>
        private const string LightAttributeNameColor = "\\red253\\green52\\blue0";

        /// <summary>Field LightAttributeValueColor.</summary>
        private const string LightAttributeValueColor = "\\red0\\green0\\blue255";

        /// <summary>Field LightCommentTextColor.</summary>
        private const string LightCommentTextColor = "\\red0\\green128\\blue0";

        /// <summary>Field LightDefaultColor.</summary>
        private const string LightDefaultColor = "\\red0\\green0\\blue0";

        /// <summary>Field DarkTagColor.</summary>
        private const string DarkTagColor = "\\red64\\green196\\blue255";

        /// <summary>Field DarkTagNameColor.</summary>
        private const string DarkTagNameColor = "\\red64\\green196\\blue255";

        /// <summary>Field DarkAttributeNameColor.</summary>
        private const string DarkAttributeNameColor = "\\red237\\green218\\blue192";

        /// <summary>Field DarkAttributeValueColor.</summary>
        private const string DarkAttributeValueColor = "\\red255\\green128\\blue255";

        /// <summary>Field DarkCommentTextColor.</summary>
        private const string DarkCommentTextColor = "\\red0\\green128\\blue0";

        /// <summary>Field DarkDefaultColor.</summary>
        private const string DarkDefaultColor = "\\red225\\green225\\blue225";

        /// <summary>Represents color table.</summary>
        public static readonly string ColorTable;

        static XmlSyntaxHighlightColor()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{\\colortbl");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red0\\green0\\blue255");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red163\\green21\\blue21");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red253\\green52\\blue0");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red0\\green0\\blue255");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red0\\green128\\blue0");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red0\\green0\\blue0");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red64\\green196\\blue255");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red64\\green196\\blue255");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red237\\green218\\blue192");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red255\\green128\\blue255");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red0\\green128\\blue0");
            stringBuilder.Append(";");
            stringBuilder.Append("\\red225\\green225\\blue225");
            stringBuilder.Append(";}");
            ColorTable = stringBuilder.ToString();
        }

        /// <summary>Get RTF tag color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF tag color.</returns>
        public static string RtfTagColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf1 " : "\\cf7 ";
        }

        /// <summary>Get RTF tag name color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF tag name color.</returns>
        public static string RtfTagNameColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf2 " : "\\cf8 ";
        }

        /// <summary>Get RTF attribute name color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF attribute name color.</returns>
        public static string RtfAttributeNameColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf3 " : "\\cf9 ";
        }

        /// <summary>Get RTF attribute value color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF attribute value color.</returns>
        public static string RtfAttributeValueColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf4 " : "\\cf10 ";
        }

        /// <summary>Get RTF comment color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF comment color.</returns>
        public static string RtfCommentColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf5 " : "\\cf11 ";
        }

        /// <summary>Get RTF default color.</summary>
        /// <param name="darkStyle">true to use dark style, otherwise, use light style.</param>
        /// <returns>RTF default color.</returns>
        public static string RtfDefaultColor(bool darkStyle = false)
        {
            return !darkStyle ? "\\cf6 " : "\\cf12 ";
        }
    }
}