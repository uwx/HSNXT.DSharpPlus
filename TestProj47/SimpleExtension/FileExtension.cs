// Decompiled with JetBrains decompiler
// Type: SimpleExtension.FileExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System.IO;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static byte[] FileToByteArray(this string pFilepath)
        {
            return !File.Exists(pFilepath) ? null : File.ReadAllBytes(pFilepath);
        }
    }
}