// Decompiled with JetBrains decompiler
// Type: SimpleExtension.EnumExtension
// Assembly: SimpleExtension, Version=0.0.23.0, Culture=neutral, PublicKeyToken=null
// MVID: B43B1EC6-29EF-47CC-B98E-E2FE4FC2095C
// Assembly location: ...\bin\Debug\SimpleExtension.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static string ToDescription(this Enum pEnumeration)
        {
            var customAttributes = (DescriptionAttribute[]) pEnumeration.GetType().GetField(pEnumeration.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttributes.Length == 0 ? pEnumeration.ToString() : customAttributes[0].Description;
        }

        public static List<Enum> ToList(this Enum pEnumeration)
        {
            return pEnumeration.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(fieldInfo => (Enum) fieldInfo.GetValue(pEnumeration)).ToList();
        }
    }
}