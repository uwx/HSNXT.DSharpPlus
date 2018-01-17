// Decompiled with JetBrains decompiler
// Type: SimpleExtension.ImageExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SBuffer = System.Buffer;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            return Image.FromStream(new MemoryStream(byteArrayIn));
        }

        public static string BytesToString(this byte[] bytes)
        {
            var chArray = new char[bytes.Length / 2];
            SBuffer.BlockCopy(bytes, 0, chArray, 0, bytes.Length);
            return new string(chArray);
        }

        public static ImageFormat GetImageFormat(this Image pImage)
        {
            if (pImage.RawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (pImage.RawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            if (pImage.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            if (pImage.RawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            if (pImage.RawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            if (pImage.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            if (pImage.RawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            if (pImage.RawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.Png;
            if (pImage.RawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            return ImageFormat.Wmf;
        }

        public static string GetMd5Hash(this byte[] pBytes)
        {
            using (var cryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                var hash = cryptoServiceProvider.ComputeHash(pBytes);
                var stringBuilder = new StringBuilder();
                foreach (var num in hash)
                    stringBuilder.Append(num.ToString("x2").ToLower());
                return stringBuilder.ToString();
            }
        }

        public static string GetSha256Hash(this byte[] pBytes)
        {
            using (var shA256Managed = new SHA256Managed())
                return BitConverter.ToString(shA256Managed.ComputeHash(pBytes)).Replace("-", string.Empty);
        }

        public static byte[] StringToBytes(this string str)
        {
            var numArray = new byte[str.Length * 2];
            SBuffer.BlockCopy(str.ToCharArray(), 0, numArray, 0, numArray.Length);
            return numArray;
        }

        public static byte[] ToArray(this Bitmap image, ImageFormat format)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, Equals(format, ImageFormat.MemoryBmp) ? ImageFormat.Bmp : format);
                return memoryStream.ToArray();
            }
        }
    }
}