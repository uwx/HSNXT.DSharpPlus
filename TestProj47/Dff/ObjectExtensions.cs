// Decompiled with JetBrains decompiler
// Type: dff.Extensions.ObjectExtensions
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\dff.Extensions.dll

using System;
using dff.Extensions.HelperClasses;

namespace TestProj47
{
    public static partial class Extensions
    {
        public static string Dump(this object obj)
        {
            try
            {
                return ObjectDumper.Dump(obj);
            }
            catch (Exception ex)
            {
                return "Error while dumping: " + ex.Message;
            }
        }
    }
}