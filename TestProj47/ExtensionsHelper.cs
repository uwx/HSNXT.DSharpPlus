// Decompiled with JetBrains decompiler
// Type: ExtensionsHelper.ExtensionMethods
// Assembly: ExtensionsHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CBB71023-6F30-4742-BBDC-1F25F766135A
// Assembly location: C:\Users\Rafael\Documents\GitHub\TestProject\TestProj47\bin\Debug\ExtensionsHelper.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestProj47
{
    public static partial class Extensions
    {
        public static char LastChar(this string input)
        {
            return string.IsNullOrEmpty(input) ? (char)0 : input[input.Length - 1];
        }
        
        public static char FirstChar(this string input)
        {
            return string.IsNullOrEmpty(input) ? (char)0 : input[0];
        }
        
        public static string ChopRight(this string input, int amt)
        {
            return string.IsNullOrEmpty(input) ? input : input.Substring(0, input.Length - amt);
        }
        
        public static void PushSplit(this IList<string> list, string s)
        {
            list.AddAll(s.Contains("\r\n") ? s.Split("\r\n") : s.Split('\n'));
        }
        
        public static bool IsDate(this string input)
        {
            return !string.IsNullOrEmpty(input) && DateTime.TryParse(input, out _);
        }

        public static string EncryptAes(this string inText, string password)
        {
            var bytes = Encoding.Unicode.GetBytes(inText);
            using (var aes = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[]
                {
                    73,
                    118,
                    97,
                    110,
                    32,
                    77,
                    101,
                    100,
                    118,
                    101,
                    100,
                    101,
                    118
                });
                if (aes == null) return inText;
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream =
                        new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                    }
                    inText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return inText;
        }

        public static string DecryptAes(this string cryptTxt, string password)
        {
            cryptTxt = cryptTxt.Replace(" ", "+");
            var buffer = Convert.FromBase64String(cryptTxt);
            using (var aes = Aes.Create())
            {
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[]
                {
                    73,
                    118,
                    97,
                    110,
                    32,
                    77,
                    101,
                    100,
                    118,
                    101,
                    100,
                    101,
                    118
                });
                if (aes == null) return cryptTxt;
                aes.Key = rfc2898DeriveBytes.GetBytes(32);
                aes.IV = rfc2898DeriveBytes.GetBytes(16);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream =
                        new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(buffer, 0, buffer.Length);
                        cryptoStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(memoryStream.ToArray());
                }
            }
            return cryptTxt;
        }

        public static string FormatString(this string value, object arg0)
        {
            return arg0 == null ? value : string.Format(value, arg0);
        }

        public static bool IsEmailAddress(this string str)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(str);
        }

        public static T Parse<T>(this string value)
        {
            var obj = default(T);
            if (!string.IsNullOrEmpty(value))
                obj = (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
            return obj;
        }

        public static string ToSlug(this string text)
        {
            var stringBuilder = new StringBuilder();
            foreach (var ch in text)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(ch);
            }
            return Regex
                .Replace(
                    Regex.Replace(Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(text)),
                        @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "_").ToLowerInvariant();
        }

        public static string Ellipsis(this string s, int charsToDisplay)
        {
            if (!string.IsNullOrWhiteSpace(s))
                return s.Length <= charsToDisplay ? s : new string(s.Take(charsToDisplay).ToArray()) + "...";
            return string.Empty;
        }

        public static int ToInt32(this string textToConvert)
        {
            int.TryParse(textToConvert, out var result);
            return result;
        }

        public static long ToInt64(this string textToConvert)
        {
            long.TryParse(textToConvert, out var result);
            return result;
        }

        public static object ToNull(this object o)
        {
            return null;
        }

        public static string SerializeToXml(this object obj)
        {
            var xdocument = new XDocument();
            using (var writer = xdocument.CreateWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize(writer, obj);
                writer.Close();
            }
            return xdocument.ToString();
        }

        public static string ToHtml(this DataTable dt)
        {
            var str1 = "<table>" + "<tr>";
            for (var index = 0; index < dt.Columns.Count; ++index)
                str1 = str1 + "<td>" + dt.Columns[index].ColumnName + "</td>";
            var str2 = str1 + "</tr>";
            for (var index1 = 0; index1 < dt.Rows.Count; ++index1)
            {
                var str3 = str2 + "<tr>";
                for (var index2 = 0; index2 < dt.Columns.Count; ++index2)
                    str3 = str3 + "<td>" + dt.Rows[index1][index2] + "</td>";
                str2 = str3 + "</tr>";
            }
            return str2 + "</table>";
        }

        public static string ToCsv(this DataTable dt, string delimiter, bool includeHeader, bool writeToFile,
            string filepath = "")
        {
            if (writeToFile && string.IsNullOrEmpty(filepath))
                throw new ArgumentException("You didn't Provide the filepath where the file will be written");
            var stringBuilder = new StringBuilder();
            if (includeHeader)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    stringBuilder.Append(column.ColumnName);
                    stringBuilder.Append(delimiter);
                }
                stringBuilder.Remove(--stringBuilder.Length, 0);
                stringBuilder.Append(Environment.NewLine);
            }
            foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
            {
                foreach (var obj in row.ItemArray)
                {
                    if (obj is DBNull)
                    {
                        stringBuilder.Append(delimiter);
                    }
                    else
                    {
                        var str = "\"" + obj.ToString().Replace("\"", "\"\"") + "\"";
                        stringBuilder.Append(str + delimiter);
                    }
                }
                stringBuilder.Remove(--stringBuilder.Length, 0);
                stringBuilder.Append(Environment.NewLine);
            }
            if (writeToFile)
            {
                using (var streamWriter = new StreamWriter(filepath, true))
                    streamWriter.Write(stringBuilder.ToString());
            }
            return stringBuilder.ToString();
        }

        public static DataTable DeleteDuplicated(this DataTable dt, string keyColName)
        {
            var dataTable = dt.Clone();
            foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
            {
                var caseIdToTest = row[keyColName].ToString();
                if (dataTable.Rows.Cast<DataRow>().All(row2 => row2[keyColName].ToString() != caseIdToTest))
                    dataTable.ImportRow(row);
            }
            return dataTable;
        }

        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            return dt.Rows.Cast<DataRow>().Select(GetItem<T>).ToList();
        }

        private static T GetItem<T>(DataRow dr)
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.Name == column.ColumnName)
                        property.SetValue(instance,
                            dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName], null);
                }
            }
            return instance;
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
        {
            return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            var dataTable = new DataTable();
            var propertyInfoArray = (PropertyInfo[]) null;
            if (list == null)
                return dataTable;
            foreach (var obj in list)
            {
                if (propertyInfoArray == null)
                {
                    propertyInfoArray = obj.GetType().GetProperties();
                    foreach (var propertyInfo in propertyInfoArray)
                    {
                        var dataType = propertyInfo.PropertyType;
                        if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            dataType = dataType.GetGenericArguments()[0];
                        dataTable.Columns.Add(new DataColumn(propertyInfo.Name, dataType));
                    }
                }
                var row = dataTable.NewRow();
                foreach (var propertyInfo in propertyInfoArray)
                    row[propertyInfo.Name] = propertyInfo.GetValue(obj, null) ?? DBNull.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static string ToCsv<T>(this IEnumerable<T> list, string delimiter, bool includeHeader, bool writeToFile,
            string filepath = "")
        {
            return list.ToDataTable().ToCsv(delimiter, includeHeader, writeToFile, filepath);
        }

        public static string ToLogString(this Exception ex, string additionalMessage = "")
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(additionalMessage))
            {
                stringBuilder.Append(additionalMessage);
                stringBuilder.Append(Environment.NewLine);
            }
            var exception = ex;
            stringBuilder.Append(Environment.NewLine);
            for (; exception != null; exception = exception.InnerException)
            {
                stringBuilder.Append(exception.Message);
                stringBuilder.Append(Environment.NewLine);
            }
            foreach (object obj in ex.Data)
            {
                stringBuilder.Append("Data :");
                stringBuilder.Append(obj);
                stringBuilder.Append(Environment.NewLine);
            }
            if (ex.StackTrace != null)
            {
                stringBuilder.Append("StackTrace:");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(ex.StackTrace);
                stringBuilder.Append(Environment.NewLine);
            }
            if (ex.Source != null)
            {
                stringBuilder.Append("Source:");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(ex.Source);
                stringBuilder.Append(Environment.NewLine);
            }
            if (ex.TargetSite != null)
            {
                stringBuilder.Append("TargetSite:");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(ex.TargetSite);
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append("BaseException:");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(ex.GetBaseException());
            return stringBuilder.ToString();
        }
    }
}