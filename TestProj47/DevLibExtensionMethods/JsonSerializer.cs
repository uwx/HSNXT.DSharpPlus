// Decompiled with JetBrains decompiler
// Type: TestProj47.JsonSerializer
// Assembly: TestProj47, Version=2.17.8.0, Culture=neutral, PublicKeyToken=null
// MVID: EBD9079F-5399-47E4-A18F-3F30589453C6
// Assembly location: ...\bin\Debug\TestProj47.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace HSNXT
{
    /// <summary>
    /// Serializes objects to the JavaScript Object Notation (JSON) and deserializes JSON data to objects.
    /// </summary>
    internal static class JsonSerializer
    {
        private static readonly char[] EscapeCharacters = new char[7]
        {
            '"',
            '\\',
            '\b',
            '\f',
            '\n',
            '\r',
            '\t'
        };

        private static readonly string EscapeCharactersString = new string(EscapeCharacters);

        private static readonly PocoJsonSerializerStrategy CurrentJsonSerializerStrategy =
            new PocoJsonSerializerStrategy();

        private static readonly char[] EscapeTable = new char[93];
        /*private const int BuilderCapacity = 2000;
        private const int TokenColon = 5;
        private const int TokenComma = 6;
        private const int TokenCurlyClose = 2;
        private const int TokenCurlyOpen = 1;
        private const int TokenFalse = 10;
        private const int TokenNone = 0;
        private const int TokenNull = 11;
        private const int TokenNumber = 8;
        private const int TokenSquaredClose = 4;
        private const int TokenSquaredOpen = 3;
        private const int TokenString = 7;
        private const int TokenTrue = 9;*/

        static JsonSerializer()
        {
            EscapeTable[34] = '"';
            EscapeTable[92] = '\\';
            EscapeTable[8] = 'b';
            EscapeTable[12] = 'f';
            EscapeTable[10] = 'n';
            EscapeTable[13] = 'r';
            EscapeTable[9] = 't';
        }

        /// <summary>Deserializes the specified json.</summary>
        /// <param name="json">The json.</param>
        /// <returns>The System.Object.</returns>
        public static object Deserialize(string json)
        {
            object obj;
            if (TryDeserialize(json, out obj))
                return obj;
            throw new SerializationException("Invalid JSON string");
        }

        /// <summary>Deserializes the specified json.</summary>
        /// <param name="json">The json.</param>
        /// <param name="type">The type.</param>
        /// <returns>The System.Object.</returns>
        public static object Deserialize(string json, Type type)
        {
            var obj = Deserialize(json);
            if (type != null && (obj == null || !ReflectionUtilities.IsAssignableFrom(obj.GetType(), type)))
                return CurrentJsonSerializerStrategy.DeserializeObject(obj, type);
            return obj;
        }

        /// <summary>Deserializes the specified json.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="json">The json.</param>
        /// <returns>The System.Object.</returns>
        public static T Deserialize<T>(string json)
        {
            return (T) Deserialize(json, typeof(T));
        }

        /// <summary>Escapes to javascript string.</summary>
        /// <param name="jsonString">The json string.</param>
        /// <returns>The System.String.</returns>
        public static string EscapeToJavascriptString(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return jsonString;
            var stringBuilder = new StringBuilder();
            var index = 0;
            while (index < jsonString.Length)
            {
                var ch = jsonString[index++];
                if (ch == 92)
                {
                    if (jsonString.Length - index >= 2)
                    {
                        switch (jsonString[index])
                        {
                            case '\\':
                                stringBuilder.Append('\\');
                                ++index;
                                continue;
                            case '"':
                                stringBuilder.Append("\"");
                                ++index;
                                continue;
                            case 't':
                                stringBuilder.Append('\t');
                                ++index;
                                continue;
                            case 'b':
                                stringBuilder.Append('\b');
                                ++index;
                                continue;
                            case 'n':
                                stringBuilder.Append('\n');
                                ++index;
                                continue;
                            case 'r':
                                stringBuilder.Append('\r');
                                ++index;
                                continue;
                            default:
                                continue;
                        }
                    }
                }
                else
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }

        /// <summary>Serializes the object.</summary>
        /// <param name="json">The JSON string.</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable.</returns>
        public static string Serialize(object json)
        {
            var builder = new StringBuilder(2000);
            if (!SerializeValue(CurrentJsonSerializerStrategy, json, builder))
                return null;
            return builder.ToString();
        }

        /// <summary>Try to deserialize the specified json.</summary>
        /// <param name="json">The json.</param>
        /// <param name="obj">The object.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        public static bool TryDeserialize(string json, out object obj)
        {
            var success = true;
            if (json != null)
            {
                var charArray = json.ToCharArray();
                var index = 0;
                obj = ParseValue(charArray, ref index, ref success);
            }
            else
                obj = null;
            return success;
        }

        /// <summary>Converts from utf32.</summary>
        /// <param name="utf32">The utf32.</param>
        /// <returns>The System.String.</returns>
        private static string ConvertFromUtf32(int utf32)
        {
            if (utf32 < 0 || utf32 > 1114111)
                throw new ArgumentOutOfRangeException(nameof(utf32), "The argument must be from 0 to 0x10FFFF.");
            if (utf32 >= 55296 && utf32 <= 57343)
                throw new ArgumentOutOfRangeException(nameof(utf32),
                    "The argument must not be in surrogate pair range.");
            if (utf32 < 65536)
                return new string((char) utf32, 1);
            utf32 -= 65536;
            return new string(new char[2]
            {
                (char) ((utf32 >> 10) + 55296),
                (char) (utf32 % 1024 + 56320)
            });
        }

        /// <summary>Gets the last index of number.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <returns>The System.Int32.</returns>
        private static int GetLastIndexOfNumber(char[] json, int index)
        {
            var index1 = index;
            while (index1 < json.Length && "0123456789+-.eE".IndexOf(json[index1]) != -1)
                ++index1;
            return index1 - 1;
        }

        /// <summary>
        /// Determines if a given object is numeric in any way (can be integer, double, null, etc).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool IsNumeric(object value)
        {
            return value is sbyte || value is byte || value is short || value is ushort || (value is int || value is uint) || value is long || value is ulong || (value is float || (value is double || value is Decimal));
        }

        /// <summary>Looks the ahead.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <returns>The System.Int32.</returns>
        private static int LookAhead(char[] json, int index)
        {
            var index1 = index;
            return NextToken(json, ref index1);
        }

        /// <summary>Next token.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <returns>The System.Int32.</returns>
        private static int NextToken(char[] json, ref int index)
        {
            RemoveWhitespace(json, ref index);
            if (index == json.Length)
                return 0;
            var ch = json[index];
            ++index;
            switch (ch)
            {
                case '"':
                    return 7;
                case ',':
                    return 6;
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return 8;
                case ':':
                    return 5;
                case '[':
                    return 3;
                case ']':
                    return 4;
                case '{':
                    return 1;
                case '}':
                    return 2;
                default:
                    --index;
                    var num = json.Length - index;
                    if (num >= 5 && json[index] == 102 && json[index + 1] == 97 && json[index + 2] == 108 && json[index + 3] == 115 && json[index + 4] == 101)
                    {
                        index += 5;
                        return 10;
                    }
                    if (num >= 4 && json[index] == 116 && json[index + 1] == 114 && json[index + 2] == 117 &&
                        json[index + 3] == 101)
                    {
                        index += 4;
                        return 9;
                    }
                    if (num < 4 || json[index] != 110 || json[index + 1] != 117 || json[index + 2] != 108 ||
                        json[index + 3] != 108)
                        return 0;
                    index += 4;
                    return 11;
            }
        }

        /// <summary>Parses the array.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <param name="success">true if succeeded; otherwise, false.</param>
        /// <returns>The JsonArray.</returns>
        private static JsonArray ParseArray(char[] json, ref int index, ref bool success)
        {
            var jsonArray = new JsonArray();
            NextToken(json, ref index);
            while (true)
            {
                switch (LookAhead(json, index))
                {
                    case 0:
                        success = false;
                        return null;
                    case 6:
                        NextToken(json, ref index);
                        continue;
                    case 4:
                        NextToken(json, ref index);
                        return jsonArray;
                    default:
                        var obj = ParseValue(json, ref index, ref success);
                        if (!success)
                            return null;
                        jsonArray.Add(obj);
                        continue;
                }
            }
        }

        /// <summary>Parses the number.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <param name="success">true if succeeded; otherwise, false.</param>
        /// <returns>The System.Object.</returns>
        private static object ParseNumber(char[] json, ref int index, ref bool success)
        {
            RemoveWhitespace(json, ref index);
            var lastIndexOfNumber = GetLastIndexOfNumber(json, index);
            var length = lastIndexOfNumber - index + 1;
            var str = new string(json, index, length);
            object obj;
            if (str.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 ||
                str.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
            {
                success = double.TryParse(new string(json, index, length), NumberStyles.Any,
                    CultureInfo.InvariantCulture, out var result);
                obj = result;
            }
            else
            {
                long result;
                success = long.TryParse(new string(json, index, length), NumberStyles.Any, CultureInfo.InvariantCulture,
                    out result);
                obj = result;
            }
            index = lastIndexOfNumber + 1;
            return obj;
        }

        /// <summary>Parses the object.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <param name="success">true if succeeded; otherwise, false.</param>
        /// <returns>The IDictionary.</returns>
        private static IDictionary<string, object> ParseObject(char[] json, ref int index, ref bool success)
        {
            IDictionary<string, object> dictionary = new JsonObject(StringComparer.OrdinalIgnoreCase);
            NextToken(json, ref index);
            while (true)
            {
                switch (LookAhead(json, index))
                {
                    case 0:
                        success = false;
                        return null;
                    case 6:
                        NextToken(json, ref index);
                        continue;
                    case 2:
                        NextToken(json, ref index);
                        return dictionary;
                    default:
                        var index1 = ParseString(json, ref index, ref success);
                        if (!success)
                        {
                            success = false;
                            return null;
                        }
                        if (NextToken(json, ref index) != 5)
                        {
                            success = false;
                            return null;
                        }
                        var obj = ParseValue(json, ref index, ref success);
                        if (!success)
                        {
                            success = false;
                            return null;
                        }
                        dictionary[index1] = obj;
                        continue;
                }
            }
        }

        /// <summary>Parses the string.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <param name="success">true if succeeded; otherwise, false.</param>
        /// <returns>The System.String.</returns>
        private static string ParseString(char[] json, ref int index, ref bool success)
        {
            var stringBuilder = new StringBuilder(2000);
            RemoveWhitespace(json, ref index);
            var ch1 = json[index++];
            var flag = false;
            while (!flag && index != json.Length)
            {
                var ch2 = json[index++];
                switch (ch2)
                {
                    case '"':
                        flag = true;
                        goto label_23;
                    case '\\':
                        if (index != json.Length)
                        {
                            switch (json[index++])
                            {
                                case '"':
                                    stringBuilder.Append('"');
                                    continue;
                                case '\\':
                                    stringBuilder.Append('\\');
                                    continue;
                                case '/':
                                    stringBuilder.Append('/');
                                    continue;
                                case 'b':
                                    stringBuilder.Append('\b');
                                    continue;
                                case 'f':
                                    stringBuilder.Append('\f');
                                    continue;
                                case 'n':
                                    stringBuilder.Append('\n');
                                    continue;
                                case 'r':
                                    stringBuilder.Append('\r');
                                    continue;
                                case 't':
                                    stringBuilder.Append('\t');
                                    continue;
                                case 'u':
                                    if (json.Length - index >= 4)
                                    {
                                        uint result1;
                                        if (!(success = uint.TryParse(new string(json, index, 4),
                                            NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result1)))
                                            return string.Empty;
                                        if (result1 >= 55296U && result1 <= 56319U)
                                        {
                                            index += 4;
                                            uint result2;
                                            if (json.Length - index >= 6 && new string(json, index, 2) == "\\u" &&
                                                uint.TryParse(new string(json, index + 2, 4), NumberStyles.HexNumber,
                                                    CultureInfo.InvariantCulture, out result2) && result2 >= 56320U &&
                                                result2 <= 57343U)
                                            {
                                                stringBuilder.Append((char) result1);
                                                stringBuilder.Append((char) result2);
                                                index += 6;
                                                continue;
                                            }
                                            success = false;
                                            return string.Empty;
                                        }
                                        stringBuilder.Append(ConvertFromUtf32((int) result1));
                                        index += 4;
                                        continue;
                                    }
                                    goto label_23;
                                default:
                                    continue;
                            }
                        }
                        else
                            goto label_23;
                    default:
                        stringBuilder.Append(ch2);
                        continue;
                }
            }
            label_23:
            if (flag)
                return stringBuilder.ToString();
            success = false;
            return null;
        }

        /// <summary>Parses the value.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        /// <param name="success">true if succeeded; otherwise, false.</param>
        /// <returns>The System.Object.</returns>
        private static object ParseValue(char[] json, ref int index, ref bool success)
        {
            switch (LookAhead(json, index))
            {
                case 1:
                    return ParseObject(json, ref index, ref success);
                case 3:
                    return ParseArray(json, ref index, ref success);
                case 7:
                    return ParseString(json, ref index, ref success);
                case 8:
                    return ParseNumber(json, ref index, ref success);
                case 9:
                    NextToken(json, ref index);
                    return true;
                case 10:
                    NextToken(json, ref index);
                    return false;
                case 11:
                    NextToken(json, ref index);
                    return null;
                default:
                    success = false;
                    return null;
            }
        }

        /// <summary>Removes the whitespace.</summary>
        /// <param name="json">The json.</param>
        /// <param name="index">The index.</param>
        private static void RemoveWhitespace(char[] json, ref int index)
        {
            while (index < json.Length && " \t\n\r\b\f".IndexOf(json[index]) != -1)
                ++index;
        }

        /// <summary>Serializes the array.</summary>
        /// <param name="jsonSerializerStrategy">The json serializer strategy.</param>
        /// <param name="sourceArray">An array.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool SerializeArray(PocoJsonSerializerStrategy jsonSerializerStrategy, IEnumerable sourceArray,
            StringBuilder builder)
        {
            builder.Append("[");
            var flag = true;
            foreach (var source in sourceArray)
            {
                if (!flag)
                    builder.Append(",");
                if (!SerializeValue(jsonSerializerStrategy, source, builder))
                    return false;
                flag = false;
            }
            builder.Append("]");
            return true;
        }

        /// <summary>Serializes the number.</summary>
        /// <param name="number">The number.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool SerializeNumber(object number, StringBuilder builder)
        {
            if (number is long)
                builder.Append(((long) number).ToString(CultureInfo.InvariantCulture));
            else if (number is ulong)
                builder.Append(((ulong) number).ToString(CultureInfo.InvariantCulture));
            else if (number is int)
                builder.Append(((int) number).ToString(CultureInfo.InvariantCulture));
            else if (number is uint)
                builder.Append(((uint) number).ToString(CultureInfo.InvariantCulture));
            else if (number is Decimal)
                builder.Append(((Decimal) number).ToString(CultureInfo.InvariantCulture));
            else if (number is float)
                builder.Append(((float) number).ToString(CultureInfo.InvariantCulture));
            else
                builder.Append(Convert.ToDouble(number, CultureInfo.InvariantCulture)
                    .ToString("r", CultureInfo.InvariantCulture));
            return true;
        }

        /// <summary>Serializes the object.</summary>
        /// <param name="jsonSerializerStrategy">The json serializer strategy.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="values">The values.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool SerializeObject(PocoJsonSerializerStrategy jsonSerializerStrategy, IEnumerable keys,
            IEnumerable values, StringBuilder builder)
        {
            builder.Append("{");
            var enumerator1 = keys.GetEnumerator();
            var enumerator2 = values.GetEnumerator();
            var flag = true;
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                var current1 = enumerator1.Current;
                var current2 = enumerator2.Current;
                if (!flag)
                    builder.Append(",");
                var source = current1 as string;
                if (source != null)
                    SerializeString(source, builder);
                else if (!SerializeValue(jsonSerializerStrategy, current2, builder))
                    return false;
                builder.Append(":");
                if (!SerializeValue(jsonSerializerStrategy, current2, builder))
                    return false;
                flag = false;
            }
            builder.Append("}");
            return true;
        }

        /// <summary>Serializes the string.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool SerializeString(string source, StringBuilder builder)
        {
            if (source.IndexOfAny(EscapeCharacters) == -1)
            {
                builder.Append('"');
                builder.Append(source);
                builder.Append('"');
                return true;
            }
            builder.Append('"');
            var charCount = 0;
            var charArray = source.ToCharArray();
            for (var index = 0; index < charArray.Length; ++index)
            {
                var ch = charArray[index];
                if (ch >= EscapeTable.Length || EscapeTable[ch] == 0)
                {
                    ++charCount;
                }
                else
                {
                    if (charCount > 0)
                    {
                        builder.Append(charArray, index - charCount, charCount);
                        charCount = 0;
                    }
                    builder.Append('\\');
                    builder.Append(EscapeTable[ch]);
                }
            }
            if (charCount > 0)
                builder.Append(charArray, charArray.Length - charCount, charCount);
            builder.Append('"');
            return true;
        }

        /// <summary>Serializes the value.</summary>
        /// <param name="jsonSerializerStrategy">The json serializer strategy.</param>
        /// <param name="value">The value.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>true if succeeded; otherwise, false.</returns>
        private static bool SerializeValue(PocoJsonSerializerStrategy jsonSerializerStrategy, object value,
            StringBuilder builder)
        {
            var flag = true;
            var source = value as string;
            if (source != null)
            {
                flag = SerializeString(source, builder);
            }
            else
            {
                var dictionary1 = value as IDictionary<string, object>;
                if (dictionary1 != null)
                {
                    flag = SerializeObject(jsonSerializerStrategy, dictionary1.Keys, dictionary1.Values, builder);
                }
                else
                {
                    var dictionary2 = value as IDictionary<string, string>;
                    if (dictionary2 != null)
                    {
                        flag = SerializeObject(jsonSerializerStrategy, dictionary2.Keys, dictionary2.Values, builder);
                    }
                    else
                    {
                        var sourceArray = value as IEnumerable;
                        if (sourceArray != null)
                            flag = SerializeArray(jsonSerializerStrategy, sourceArray, builder);
                        else if (IsNumeric(value))
                            flag = SerializeNumber(value, builder);
                        else if (value is bool)
                            builder.Append((bool) value ? "true" : "false");
                        else if (value == null)
                        {
                            builder.Append("null");
                        }
                        else
                        {
                            object output;
                            flag = jsonSerializerStrategy.TrySerializeNonPrimitiveObject(value, out output);
                            if (flag)
                                SerializeValue(jsonSerializerStrategy, output, builder);
                        }
                    }
                }
            }
            return flag;
        }
    }
}