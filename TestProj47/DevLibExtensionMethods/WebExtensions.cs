﻿// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.WebExtensions
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System.IO;
using System.Net;

namespace DevLib.ExtensionMethods
{
  /// <summary>Web Extensions.</summary>
  public static class WebExtensions
  {
    /// <summary>Downloads data from a url.</summary>
    /// <param name="url">Url to retrieve the data.</param>
    /// <param name="useSystemWebProxy">true to use "IE Proxy" based on the currently impersonated user's proxy settings; false to not use proxy.</param>
    /// <returns>Byte array of data from the url.</returns>
    public static byte[] DownloadData(this string url, bool useSystemWebProxy = false)
    {
      WebRequest webRequest = WebRequest.Create(url);
      if (useSystemWebProxy)
        webRequest.Proxy = WebRequest.GetSystemWebProxy();
      using (WebResponse response = webRequest.GetResponse())
      {
        using (Stream responseStream = response.GetResponseStream())
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            byte[] buffer = new byte[1024];
            while (true)
            {
              int count = responseStream.Read(buffer, 0, buffer.Length);
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
