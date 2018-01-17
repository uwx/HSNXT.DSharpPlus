// Decompiled with JetBrains decompiler
// Type: SimpleExtension.MemoryStreamExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System.IO;
using System.Text;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ToString(this MemoryStream pMemoryStream, Encoding pEncoding)
        {
            return pEncoding.GetString(pMemoryStream.ToArray());
        }

        public static string ToString(this MemoryStream pMemoryStream)
        {
            return pMemoryStream.ToString(Encoding.Default);
        }

        public static void WriteString(this MemoryStream pMemoryStream, string pInput, Encoding pEncoding)
        {
            var bytes = pEncoding.GetBytes(pInput);
            pMemoryStream.Write(bytes, 0, bytes.Length);
        }

        public static void WriteString(this MemoryStream pMemoryStream, string pInput)
        {
            pMemoryStream.WriteString(pInput, Encoding.Default);
        }
    }
}