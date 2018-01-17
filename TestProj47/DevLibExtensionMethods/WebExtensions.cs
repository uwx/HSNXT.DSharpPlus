// Decompiled with JetBrains decompiler
// Type: TestProj47.WebExtensions
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System.IO;
using System.Net;

namespace HSNXT
{
    public static partial class Extensions

    {
        /// <summary>Downloads data from a url.</summary>
        /// <param name="url">Url to retrieve the data.</param>
        /// <param name="useSystemWebProxy">true to use "IE Proxy" based on the currently impersonated user's proxy settings; false to not use proxy.</param>
        /// <returns>Byte array of data from the url.</returns>
        public static byte[] DownloadData(this string url, bool useSystemWebProxy = false)
        {
            var webRequest = WebRequest.Create(url);
            if (useSystemWebProxy)
                webRequest.Proxy = WebRequest.GetSystemWebProxy();
            using (var response = webRequest.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var buffer = new byte[1024];
                        while (true)
                        {
                            var count = responseStream.Read(buffer, 0, buffer.Length);
                            if (count != 0)
                                memoryStream.Write(buffer, 0, count);
                            else
                                break;
                        }
                        memoryStream.Flush();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}