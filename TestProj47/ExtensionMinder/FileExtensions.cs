// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.FileExtensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System.IO;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ToJavaScriptExtension(this string str, bool justdoIt = false)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            if (Path.HasExtension(str) && str.IsFileExtensionValid() && !justdoIt)
                return Path.ChangeExtension(str, FileTypeExtensions.Js);
            return str + FileTypeExtensions.Js;
        }

        public static bool IsFileExtensionValid(this string fExt)
        {
            var flag = true;
            if (!string.IsNullOrWhiteSpace(fExt) && fExt.Length > 1 && fExt[0] == 46)
            {
                foreach (var invalidFileNameChar in Path.GetInvalidFileNameChars())
                {
                    if (fExt.Contains(invalidFileNameChar))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        public static class FileTypeExtensions
        {
            public static string Jpg = ".jpg";
            public static string Jpeg = ".jpeg";
            public static string Png = ".png";
            public static string Gif = ".gif";
            public static string Pdf = ".pdf";
            public static string Doc = ".doc";
            public static string Docx = ".docx";
            public static string Xls = ".xls";
            public static string Xlsx = ".xlsx";
            public static string Ppt = ".ppt";
            public static string Pptx = ".pptx";
            public static string Js = ".js";
            public static string Css = ".css";
            public static string Bmp = ".bmp";
            public static string Wmv = ".wmv";
            public static string Swf = ".swf";
        }
    }
}