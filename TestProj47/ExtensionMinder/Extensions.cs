// Decompiled with JetBrains decompiler
// Type: ExtensionMinder.Extensions
// Assembly: ExtensionMinder, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9895DAEA-F52E-4F0C-B5FD-FB311AEFF61C
// Assembly location: ...\bin\Debug\ExtensionMinder.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ConstantConditionalAccessQualifier

namespace HSNXT
{
    public static partial class Extensions
    {
        public static readonly Type[] PredefinedTypes =
        {
            typeof(object),
            typeof(bool),
            typeof(char),
            typeof(string),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        };

        public static string ToLowerInvariantWithOutSpaces(this string s)
        {
            return s.ToLowerInvariant().Replace(" ", string.Empty).Trim();
        }

        public static string TrimToLength(this string s, int length)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length <= length)
                return s;
            return s.Substring(0, length);
        }

        public static string MakeValidXml(this string s)
        {
            return Regex.Replace(s, @"(?<=\<\w+)[#\{\}\(\)\&](?=\>)|(?<=\</\w+)[#\{\}\(\)\&](?=\>)", "-");
        }

        public static string MakeValidUrl(this string s)
        {
            s = Regex.Replace(s, @"[^a-z0-9\s-]", "");
            s = Regex.Replace(s, @"\s+", " ").Trim();
            s = Regex.Replace(s, @"\s", "-");
            return s;
        }

        public static string ToDefaultString(this string s, string defaultText)
        {
            return string.IsNullOrWhiteSpace(s) ? defaultText.Trim() : s.Trim();
        }

        public static string RemoveJunkWordsFromNumber(this string s)
        {
            s = s.Replace("years", string.Empty);
            s = s.Replace("year", string.Empty);
            s = s.Replace("%", string.Empty);
            s = s.Replace("$", string.Empty);
            s = s.Replace("-", string.Empty);
            return s;
        }

        public static string MakeValidFileName(this string s)
        {
            return Path.GetInvalidFileNameChars().Aggregate(s, (current, c) => current.Replace(c, '_'));
        }

        public static Hashtable ConvertPropertiesAndValuesToHashtable(this object obj)
        {
            var hashtable = new Hashtable();
            var array = obj.GetType().GetProperties().Where(a => a.MemberType.Equals(MemberTypes.Property)).ToArray();
            Array.Sort(array,
                (propertyInfo1, propertyInfo2) =>
                    string.Compare(propertyInfo1.Name, propertyInfo2.Name, StringComparison.Ordinal));
            foreach (var propertyInfo in array)
                hashtable.Add(propertyInfo.Name,
                    propertyInfo.GetValue(obj, BindingFlags.Public, null, null, CultureInfo.CurrentCulture));
            return hashtable;
        }

        public static string FirstSortableProperty(this Type type)
        {
            var propertyInfo =
                type.GetProperties().FirstOrDefault(property => property.PropertyType.IsPredefinedType());
            if (propertyInfo == null)
                throw new NotSupportedException("Cannot find property to sort by.");
            return propertyInfo.Name;
        }

        internal static bool IsPredefinedType(this Type type)
        {
            return PredefinedTypes.Any(t => t == type);
        }

        public static Guid IfGuidEmptyCreateNew(this Guid guid)
        {
            if (guid == Guid.Empty)
                return Guid.NewGuid();
            return guid;
        }

        public static string RelativeFormat(this DateTime source)
        {
            return source.RelativeFormat(string.Empty);
        }

        public static string RelativeFormat(this DateTime source, string defaultFormat)
        {
            var timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            var totalSeconds = timeSpan.TotalSeconds;
            string str;
            if (totalSeconds > 0.0)
            {
                if (totalSeconds < 60.0)
                    str = timeSpan.Seconds == 1 ? "one second ago" : timeSpan.Seconds + " seconds ago";
                else if (totalSeconds < 120.0)
                    str = "a minute ago";
                else if (totalSeconds < 2700.0)
                    str = timeSpan.Minutes + " minutes ago";
                else if (totalSeconds < 5400.0)
                    str = "an hour ago";
                else if (totalSeconds < 86400.0)
                {
                    var num = timeSpan.Hours;
                    if (num == 1)
                        num = 2;
                    str = num + " hours ago";
                }
                else if (totalSeconds < 172800.0)
                    str = "yesterday";
                else if (totalSeconds < 2592000.0)
                    str = timeSpan.Days + " days ago";
                else if (totalSeconds < 31104000.0)
                {
                    var int32 = Convert.ToInt32(Math.Floor(timeSpan.Days / 30.0));
                    str = int32 <= 1 ? "one month ago" : int32 + " months ago";
                }
                else
                {
                    var int32 = Convert.ToInt32(Math.Floor(timeSpan.Days / 365.0));
                    str = int32 <= 1 ? "one year ago" : int32 + " years ago";
                }
            }
            else
            {
                var dateTime = source;
                str = string.IsNullOrEmpty(defaultFormat)
                    ? dateTime.ToStringCurrent()
                    : dateTime.ToString(defaultFormat);
            }
            return str;
        }

        public static int ToXmlBoolean(this bool value)
        {
            return !value ? 0 : 1;
        }

        public static string ToSentenceCase(this string str)
        {
            return str.Length == 0
                ? str
                : new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture).Replace(str.ToLower(),
                    s => s.Value.ToUpper());
        }

        public static string AddSpacesToSentence(this string text)
        {
            return Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");
        }

        public static string ToPascalCase(this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str), "Null text cannot be converted!");
            if (str.Length == 0)
                return str;
            var strArray = str.Split(' ');
            for (var index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].Length <= 0) continue;
                var str1 = strArray[index];
                var upper = char.ToUpper(str1[0]);
                strArray[index] = (int) upper + str1.Substring(1);
            }
            return string.Join(string.Empty, strArray);
        }

        public static string ToQueryString(this NameValueCollection coll)
        {
            return string.Join("&", coll.Cast<string>().Select(a =>
                $"{HttpUtility.UrlEncode(a)}={HttpUtility.UrlEncode(coll[a])}"));
        }

        public static PropertyInfo GetProperty<TX, TY>(this TX obj, Expression<Func<TX, TY>> selector)
        {
            Expression expression = selector;
            if (expression is LambdaExpression)
                expression = ((LambdaExpression) expression).Body;
            if (expression.NodeType == ExpressionType.MemberAccess)
                return (PropertyInfo) ((MemberExpression) expression).Member;
            throw new InvalidOperationException();
        }

        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            var attributeType = typeof(T);
            return (T) property.GetCustomAttributes(attributeType, false).FirstOrDefault();
        }

        public static bool IsSystem(this PropertyInfo property)
        {
            return property.PropertyType.Namespace != null && property.PropertyType.Namespace.StartsWith("System");
        }

        public static T To<T>(this string value)
        {
            if (typeof(T).IsEnum)
            {
                if (string.IsNullOrEmpty(value))
                    return default;
                return (T) Enum.Parse(typeof(T), value);
            }
            if (!string.IsNullOrEmpty(value))
                return (T) Convert.ChangeType(value, typeof(T));
            return default;
        }

        public static string ShowIf(this string answer, Func<bool> question)
        {
            return !question() ? "" : answer;
        }

        public static string GetContent(this Stream stream)
        {
            if (stream == null)
                throw new Exception("No stream available for the request");
            return new StreamReader(stream).ReadToEnd();
        }

        public static void ToFile(this Stream stream, string filepath)
        {
            var fileInfo = new FileInfo(filepath);
            fileInfo.Directory?.Create();
            using (var fileStream = File.Create(filepath))
                stream.CopyTo(fileStream);
        }
    }
}