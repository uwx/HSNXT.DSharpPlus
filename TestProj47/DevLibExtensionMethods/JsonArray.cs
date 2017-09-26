// Decompiled with JetBrains decompiler
// Type: DevLib.ExtensionMethods.JsonArray
// Assembly: DevLib.ExtensionMethods, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\DevLib.ExtensionMethods.dll

using System.Collections.Generic;

namespace DevLib.ExtensionMethods
{
  /// <summary>Represents the JSON array.</summary>
  internal class JsonArray : List<object>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:DevLib.ExtensionMethods.JsonArray" /> class.
    /// </summary>
    public JsonArray()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:DevLib.ExtensionMethods.JsonArray" /> class.
    /// </summary>
    /// <param name="capacity">The capacity of the json array.</param>
    public JsonArray(int capacity)
      : base(capacity)
    {
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return JsonSerializer.Serialize((object) this) ?? string.Empty;
    }
  }
}
