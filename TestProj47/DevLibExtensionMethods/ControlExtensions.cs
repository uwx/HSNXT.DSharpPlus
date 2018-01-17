#if NetFX
// Decompiled with JetBrains decompiler
// Type: TestProj47.ControlExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Windows.Forms;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Resizes the width of the columns.</summary>
        /// <param name="source">ListView object.</param>
        public static void AutoResizeColumns(this ListView source)
        {
            source.BeginUpdate();
            try
            {
                foreach (ColumnHeader column in source.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    var width1 = column.Width;
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    var width2 = column.Width;
                    column.Width = Math.Max(width1, width2);
                }
            }
            finally
            {
                source.EndUpdate();
            }
        }

        /// <summary>Highlights the Xml syntax for RichTextBox.</summary>
        /// <param name="source">The RichTextBox instance.</param>
        /// <param name="indentXml">true to indent Xml string; otherwise, keep the original string.</param>
        /// <param name="darkStyle">true to use dark style; otherwise, use light style.</param>
        public static void HighlightXmlSyntax(this RichTextBox source, bool indentXml = true, bool darkStyle = false)
        {
            if (!source.Text.IsValidXml())
                return;
            if (indentXml)
                source.Text = source.Text.ToIndentXml(false);
            source.Rtf = source.Rtf.ToXmlSyntaxHighlightRtf(indentXml, false, false);
        }
    }
}
#endif