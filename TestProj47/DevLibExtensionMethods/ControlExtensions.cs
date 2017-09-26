// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.ControlExtensions
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System;
using System.Windows.Forms;

namespace DevLib.ExtensionMethods
{
  /// <summary>Control Extensions.</summary>
  public static class ControlExtensions
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
          int width1 = column.Width;
          column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
          int width2 = column.Width;
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
