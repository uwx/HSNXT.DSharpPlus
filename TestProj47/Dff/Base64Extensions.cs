// Decompiled with JetBrains decompiler
// Type: dff.Extensions.Base64Extensions
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string BitmapToBase64(this Image image)
        {
            try
            {
                var memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                memoryStream.Close();
                memoryStream.Dispose();
                return base64String;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}