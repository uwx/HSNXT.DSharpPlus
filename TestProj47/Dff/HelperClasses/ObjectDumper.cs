// Decompiled with JetBrains decompiler
// Type: dff.Extensions.HelperClasses.ObjectDumper
// Assembly: dff.Extensions, Version=1.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6C927DF-93D7-4A34-9061-9B93EC850F98
// Assembly location: ...\bin\Debug\dff.Extensions.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HSNXT.dff.Extensions.HelperClasses
{
    internal sealed class ObjectDumper
    {
        private int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private readonly List<int> _hashListOfFoundElements;

        private ObjectDumper(int indentSize)
        {
            this._indentSize = indentSize;
            this._stringBuilder = new StringBuilder();
            this._hashListOfFoundElements = new List<int>();
        }

        public static string Dump(object element)
        {
            return Dump(element, 2);
        }

        public static string Dump(object element, int indentSize)
        {
            return new ObjectDumper(indentSize).DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                this.Write(FormatValue(element));
            }
            else
            {
                var type = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(type))
                {
                    this.Write("{{{0}}}", (object) type.FullName);
                    this._hashListOfFoundElements.Add(element.GetHashCode());
                    ++this._level;
                }
                if (element is IEnumerable enumerable)
                {
                    foreach (var element1 in enumerable)
                    {
                        if (element1 is IEnumerable && !(element1 is string))
                        {
                            ++this._level;
                            this.DumpElement(element1);
                            --this._level;
                        }
                        else if (!this.AlreadyTouched(element1))
                            this.DumpElement(element1);
                        else
                            this.Write("{{{0}}} <-- bidirectional reference found",
                                (object) element1.GetType().FullName);
                    }
                }
                else
                {
                    foreach (var member in element.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public))
                    {
                        var fieldInfo = member as FieldInfo;
                        var propertyInfo = member as PropertyInfo;
                        if ((object) fieldInfo == null && (object) propertyInfo == null) continue;
                        var c = (object) fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        var obj = (object) fieldInfo != null
                            ? fieldInfo.GetValue(element)
                            : propertyInfo.GetValue(element, null);
                        if (c.IsValueType || c == typeof(string))
                        {
                            this.Write("{0}: {1}", (object) member.Name, (object) FormatValue(obj));
                        }
                        else
                        {
                            var flag1 = typeof(IEnumerable).IsAssignableFrom(c);
                            this.Write("{0}: {1}", (object) member.Name, flag1 ? (object) "..." : (object) "{ }");
                            var flag2 = !flag1 && this.AlreadyTouched(obj);
                            ++this._level;
                            if (!flag2)
                                this.DumpElement(obj);
                            else
                                this.Write("{{{0}}} <-- bidirectional reference found",
                                    (object) obj.GetType().FullName);
                            --this._level;
                        }
                    }
                }
                if (!typeof(IEnumerable).IsAssignableFrom(type))
                    --this._level;
            }
            return this._stringBuilder.ToString();
        }

        private bool AlreadyTouched(object value)
        {
            var hashCode = value.GetHashCode();
            return this._hashListOfFoundElements.Any(t => t == hashCode);
        }

        private void Write(string value, params object[] args)
        {
            var str = new string(' ', this._level * this._indentSize);
            if (args != null)
                value = string.Format(value, args);
            this._stringBuilder.AppendLine(str + value);
        }

        private static string FormatValue(object o)
        {
            switch (o)
            {
                case null:
                    return "null";
                case DateTime time:
                    return time.ToString("yyyy-MM-dd HH:mm:ss");
                case string _:
                    return $"\"{o}\"";
                case ValueType _:
                    return o.ToString();
            }
            return o is IEnumerable ? "..." : "{ }";
        }
    }
}